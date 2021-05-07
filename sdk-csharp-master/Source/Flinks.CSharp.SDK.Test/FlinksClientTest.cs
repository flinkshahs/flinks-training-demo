// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Flinks.CSharp.SDK.Model.Attributes;
using Flinks.CSharp.SDK.Model.Authorize;
using Flinks.CSharp.SDK.Model.Constant;
using Flinks.CSharp.SDK.Model.DeleteCard;
using Flinks.CSharp.SDK.Model.Enums;
using Flinks.CSharp.SDK.Model.GetAccountsDetail;
using Flinks.CSharp.SDK.Model.GetAccountsSummary;
using Flinks.CSharp.SDK.Model.GetStatement;
using Flinks.CSharp.SDK.Model.Score;
using Flinks.CSharp.SDK.Model.Shared;

namespace Flinks.CSharp.SDK.Test
{
    public class FlinksClientTest : FlinksTestBase
    {
        [Theory]
        [MemberData(nameof(FlinksClientInstantiationValueTest.TestData), MemberType =
            typeof(FlinksClientInstantiationValueTest))]
        public void Should_throw_an_exception_if_customerId_or_endpoint_is_null_or_empty(string customerId,
            string endpoint)
        {
            Assert.Throws<NullReferenceException>(() => new FlinksClient(customerId, endpoint));
        }

        [Fact(Skip = "On Sandbox this feature is disabled.")]
        public void Should_retrieve_an_auth_token()
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var secretKey = "I am an auth token";

            var response = apiClient.GenerateAuthorizeToken(secretKey);

            Assert.Equal(200, response.HttpStatusCode);
            Assert.NotNull(response.Token);
        }

        [Fact]
        public void Should_return_an_error_when_sending_wrong_secret_when_trying_to_get_an_auth_token()
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authToken = "I am an wrong auth token";

            var response = apiClient.GenerateAuthorizeToken(authToken);

            Assert.Equal(401, response.HttpStatusCode);
            Assert.Equal("UNAUTHORIZED", response.FlinksCode);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTestWrongPassword.TestData), MemberType = typeof(AuthorizeTestWrongPassword))]
        public void Should_Authorize_and_receive_an_not_authorized_response_from_the_api(string institution,
            string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var response = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            Assert.Equal(401, response.HttpStatusCode);
            Assert.Equal(ClientStatus.UNAUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_and_retrieve_mfa_questions_from_api(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var response = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            Assert.Equal(203, response.HttpStatusCode);
            Assert.Equal(ClientStatus.PENDING_MFA_ANSWERS, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_using_cached_flow(string institution, string userName, string password, bool? save,
            bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh,
            string tag = null)
        {
            //if we are using save = false, this test is pointless
            if (save != null && (bool) save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizationResult = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizationResult.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizationResult.SecurityChallenges);
            }

            AuthorizeResult mostRecentCachedAuthorizationResult = null;

            if (authorizationResult.RequestId != null)
            {
                var answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizationResult.RequestId,
                        authorizationResult.SecurityChallenges);

                var loginId = new Guid(answerMfaQuestionsAndAuthorizeResult.Login.Id);

                mostRecentCachedAuthorizationResult = apiClient.Authorize(loginId);
            }

            Assert.Equal(200, mostRecentCachedAuthorizationResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_and_not_retrieve_mfa_questions_from_api_with_WithMfaQuestions(string institution,
            string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResult = apiClient.Authorize(institution, userName, password, save, mostRecentCached, true,
                requestLanguage, scheduleRefresh, tag);

            Assert.Equal(203, authorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.PENDING_MFA_ANSWERS, apiClient.ClientStatus);
            Assert.NotEmpty(authorizeResult.SecurityChallenges);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_and_not_retrieve_mfa_questions_from_api_in_french(string institution,
            string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResult = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, RequestLanguage.fr, scheduleRefresh, tag);

            Assert.True(IsSecurityChallengeInFrench(authorizeResult.SecurityChallenges));
            Assert.Equal(203, authorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.PENDING_MFA_ANSWERS, apiClient.ClientStatus);
            Assert.NotEmpty(authorizeResult.SecurityChallenges);
        }

        [Theory(Skip = "On Sandbox this feature is disabled.")]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_and_set_nightly_refresh_for_card(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResult = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, null, true, tag);

            if (authorizeResult.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResult.SecurityChallenges);
            }

            if (authorizeResult.RequestId != null)
            {
                var answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResult.RequestId,
                        authorizeResult.SecurityChallenges);

                var loginId = new Guid(answerMfaQuestionsAndAuthorizeResult.Login.Id);

                var deleteCardMessage = apiClient.DeleteCard(loginId);
            }

            var authorizeResultForNightlyRefreshVerification = apiClient.Authorize(institution, userName, password,
                true, mostRecentCached, withMfaQuestions, null, true, tag);

            if (authorizeResult.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResultForNightlyRefreshVerification.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResultForNightlyRefreshVerification = null;

            if (authorizeResultForNightlyRefreshVerification.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResultForNightlyRefreshVerification =
                    apiClient.AnswerMfaQuestionsAndAuthorize(
                        (Guid) authorizeResultForNightlyRefreshVerification.RequestId,
                        authorizeResultForNightlyRefreshVerification.SecurityChallenges);
            }

            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResultForNightlyRefreshVerification?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
            Assert.NotEmpty(answerMfaQuestionsAndAuthorizeResultForNightlyRefreshVerification?.SecurityChallenges ??
                            throw new InvalidOperationException("Security Challenges can't be null."));
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_Authorize_and_add_a_tag(string institution, string userName, string password, bool? save,
            bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh,
            string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var tagValue = "I am a tag";

            var authorizeResult = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, null, true, tagValue);

            Assert.Equal(tagValue, authorizeResult.Tag);
            Assert.Equal(203, authorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.PENDING_MFA_ANSWERS, apiClient.ClientStatus);
            Assert.NotEmpty(authorizeResult.SecurityChallenges);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_AnswerMfaQuestionsAndAuthorize_and_receive_authorized_message_from_the_api(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);
            }

            Assert.Equal(authorizeResponse.RequestId, answerMfaQuestionsAndAuthorizeResult?.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_AnswerMfaQuestionsAndAuthorize_and_receive_non_authoritative_from_api(string institution,
            string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        new List<SecurityChallenge>());
            }

            Assert.Equal(203, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.PENDING_MFA_ANSWERS, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsSummaryResult(string institution, string userName, string password,
            bool? save, bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage,
            bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsSummaryResult getSummaryResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getSummaryResult = apiClient.GetAccountSummary((Guid) authorizeResponse.RequestId);
            }

            Assert.NotNull(getSummaryResult.Accounts);
            Assert.Equal(authorizeResponse.RequestId, getSummaryResult.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_throw_an_exception_if_calling_GetAccountsSummary_if_ClientStatus_is_not_authorized(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            var requestId = Guid.Empty;

            if (authorizeResponse.RequestId != null)
            {
                requestId = (Guid) authorizeResponse.RequestId;
            }

            Assert.Throws<Exception>(() => apiClient.GetAccountSummary(requestId));
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_throw_an_exception_if_calling_GetAccountsDetail_if_ClientStatus_is_not_authorized(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            var requestId = Guid.Empty;

            if (authorizeResponse.RequestId != null)
            {
                requestId = (Guid) authorizeResponse.RequestId;
            }

            Assert.Throws<Exception>(() => apiClient.GetAccountDetails(requestId, null, null, null, null, null, null));
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult(string institution, string userName, string password,
            bool? save, bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage,
            bool? scheduleRefresh, string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null,
                    null, null, null, null);
            }

            Assert.NotNull(getAccountsDetailResult?.Accounts);
            Assert.NotNull(getAccountsDetailResult.Login);
            Assert.Equal((Guid) authorizeResponse.RequestId, getAccountsDetailResult.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_WithNoAccountIdentity(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, false, null,
                    null, null, null, null);
            }

            Assert.Null(getAccountsDetailResult?.Accounts[0].AccountNumber);
            Assert.Null(getAccountsDetailResult?.Accounts[0].InstitutionNumber);
            Assert.Null(getAccountsDetailResult?.Accounts[0].TransitNumber);
            Assert.Equal(authorizeResponse.RequestId, getAccountsDetailResult?.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_with_no_KYC(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, false,
                    null, null, null, null);
            }

            Assert.Null(getAccountsDetailResult?.Accounts[0].Holder);
            Assert.Equal(authorizeResponse.RequestId, getAccountsDetailResult?.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_with_no_transactions(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null,
                    false, null, null, null);
            }

            Assert.Empty(getAccountsDetailResult.Accounts[0].Transactions);
            Assert.Equal(authorizeResponse.RequestId, getAccountsDetailResult.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_with_no_balance(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null,
                    null, false, null, null);
            }

            Assert.Null(getAccountsDetailResult?.Accounts[0].Balance.Available);
            Assert.Null(getAccountsDetailResult?.Accounts[0].Balance.Current);
            Assert.Null(getAccountsDetailResult?.Accounts[0].Balance.Limit);
            Assert.Equal(authorizeResponse.RequestId, getAccountsDetailResult?.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_with_DaysOfTransaction_Days90_and_Days365(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            var answerMfaQuestionsAndAuthorizeResult =
                apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                    authorizeResponse.SecurityChallenges);

            var getAccountsDetailResult = apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null,
                null, null, DaysOfTransaction.Days90, null);


            var apiClient2 = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse2 = apiClient2.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse2.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult2 = null;
            AccountsDetailResult getAccountsDetailResult2 = null;

            if (authorizeResponse2.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult2 =
                    apiClient2.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse2.RequestId,
                        authorizeResponse2.SecurityChallenges);

                getAccountsDetailResult2 = apiClient2.GetAccountDetails((Guid) authorizeResponse2.RequestId, null, null,
                    null, null, DaysOfTransaction.Days365, null);
            }

            Assert.NotEmpty(getAccountsDetailResult.Accounts[0].Transactions);
            Assert.NotEmpty(getAccountsDetailResult2.Accounts[0].Transactions);
            Assert.Equal(authorizeResponse.RequestId, getAccountsDetailResult.RequestId);
            Assert.Equal(authorizeResponse2.RequestId, getAccountsDetailResult2.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult2?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient2.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_retrieve_GetAccountsDetailResult_with_account_filter(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResponseWithAccountFilterResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                var getAccountSummaryResult = apiClient.GetAccountSummary((Guid) authorizeResponse.RequestId);

                if (getAccountSummaryResult != null)
                {
                    var accountId = (Guid) getAccountSummaryResult.Accounts.First().Id;

                    var apiClientForAccountFilter = new FlinksClient(CustomerId, Endpoint);

                    var authorizeResponseForAccountFilter = apiClientForAccountFilter.Authorize(institution, userName,
                        password, save, mostRecentCached, withMfaQuestions, requestLanguage, scheduleRefresh, tag);

                    if (authorizeResponseForAccountFilter.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
                    {
                        AnswerMfaQuestion(authorizeResponseForAccountFilter.SecurityChallenges);
                    }

                    if (authorizeResponseForAccountFilter.RequestId != null)
                    {
                        apiClientForAccountFilter.AnswerMfaQuestionsAndAuthorize(
                            (Guid) authorizeResponseForAccountFilter.RequestId,
                            authorizeResponseForAccountFilter.SecurityChallenges);

                        getAccountsDetailResponseWithAccountFilterResult = apiClientForAccountFilter.GetAccountDetails(
                            (Guid) authorizeResponseForAccountFilter.RequestId,
                            null,
                            null,
                            null,
                            null,
                            null,
                            new List<Guid>()
                            {
                                accountId
                            });
                    }
                }
            }

            Assert.True(getAccountsDetailResponseWithAccountFilterResult?.Accounts.Count < 2);
            Assert.NotNull(getAccountsDetailResponseWithAccountFilterResult.Accounts);
            Assert.NotNull(getAccountsDetailResponseWithAccountFilterResult.Login);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_call_GetAccountsDetailAsync_and_retrieve_GetAccountsDetailResult(string institution,
            string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            AccountsDetailResult getAccountsDetailResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null, null, null, null, null);

                getAccountsDetailResult = apiClient.GetAccountDetailsAsync((Guid) authorizeResponse.RequestId);
            }

            Assert.NotNull(getAccountsDetailResult?.Accounts);
            if (save != null && (bool) save)
            {
                Assert.NotNull(getAccountsDetailResult.Login);
            }

            Assert.Equal((Guid) authorizeResponse.RequestId, getAccountsDetailResult.RequestId);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_throw_an_exception_when_trying_to_call_GetStatements_passing_Months12_and_not_accountList(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            Assert.Throws<Exception>(() => apiClient.GetStatements(new Guid(), NumberOfStatements.Months12, null));
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void
            Should_throw_an_exception_when_trying_to_call_GetStatements_passing_Months12_and_accountList_has_more_than_one_entry(
                string institution, string userName, string password, bool? save, bool? mostRecentCached,
                bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            Assert.Throws<Exception>(() => apiClient.GetStatements(new Guid(), NumberOfStatements.Months12,
                new List<Guid>()
                {
                    new Guid(),
                    new Guid()
                }));
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_call_GetStatements_and_retrieve_the_statements(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, false, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            StatementResult statementResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                statementResult = apiClient.GetStatements((Guid) authorizeResponse.RequestId, null, null);
            }

            Assert.NotNull(statementResult?.StatementsByAccount);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_call_GetStatements_and_retrieve_the_statements_setting_up_number_of_statements_as_Month3(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, false, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            StatementResult statementResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                statementResult =
                    apiClient.GetStatements((Guid) authorizeResponse.RequestId, NumberOfStatements.Months3, null);
            }

            Assert.Equal(3, statementResult?.StatementsByAccount[0].Statements.Count);
            Assert.Equal(3, statementResult?.StatementsByAccount[1].Statements.Count);
            Assert.NotNull(statementResult?.StatementsByAccount);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void
            Should_call_GetStatements_and_retrieve_the_statements_setting_up_number_of_statements_as_Month3_with_account_filter(
                string institution, string userName, string password, bool? save, bool? mostRecentCached,
                bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, false, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            if (authorizeResponse.RequestId != null)
            {
                var answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);
            }

            var getSummaryResult = apiClient.GetAccountSummary((Guid) authorizeResponse.RequestId);

            var searchedAccount = getSummaryResult.Accounts.First().Id;



            //var accountId = new Guid("769278c5-b34c-49ab-f9b4-08d5ca4e3b3d");

            //var statementResult = apiClient.GetStatements(requestId, NumberOfStatements.Months3, new List<Guid>()
            //{
            //    accountId
            //});


            //Assert.Equal(3, statementResult.StatementsByAccount.FirstOrDefault()?.Statements.Count);
            //Assert.NotNull(statementResult.StatementsByAccount);
            //Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            //Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_call_GetStatements_and_retrieve_the_statements_setting_up_number_of_statements_as_Month12(
            string institution, string userName, string password, bool? save, bool? mostRecentCached,
            bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            StatementResult statementResult = null;
            if (authorizeResponse.RequestId != null)
            {
                apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                    authorizeResponse.SecurityChallenges);

                var getAccountsDetailResult =
                    apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null, null, null, null);

                var firstAccountId = getAccountsDetailResult?.Accounts?.FirstOrDefault()?.Id;

                if (firstAccountId != null)
                {
                    var accountId = (Guid) firstAccountId;

                    var apiClientForStatement = new FlinksClient(CustomerId, Endpoint);

                    var authorizeResponseForStatement = apiClientForStatement.Authorize(institution, userName, password,
                        save, mostRecentCached, withMfaQuestions, requestLanguage, scheduleRefresh, tag);

                    if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
                    {
                        AnswerMfaQuestion(authorizeResponseForStatement.SecurityChallenges);
                    }

                    if (authorizeResponseForStatement.RequestId != null)
                    {
                        apiClientForStatement.AnswerMfaQuestionsAndAuthorize(
                            (Guid) authorizeResponseForStatement.RequestId,
                            authorizeResponseForStatement.SecurityChallenges);

                        statementResult = apiClientForStatement.GetStatements(
                            (Guid) authorizeResponseForStatement.RequestId,
                            NumberOfStatements.Months12, new List<Guid>()
                            {
                                accountId
                            });
                    }
                }
            }

            Assert.Equal(12, statementResult?.StatementsByAccount.FirstOrDefault()?.Statements.Count);
            Assert.NotNull(statementResult?.StatementsByAccount);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory(Skip = "On Sandbox this feature is disabled.")]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_call_GetStatementsAsync_and_retrieve_the_statements(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            //TODO: fix
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            var requestId = new Guid();

            apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                authorizeResponse.SecurityChallenges);

            var getAccountsDetailResult =
                apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null, null, null, null);

            var firstAccountId = getAccountsDetailResult?.Accounts?.FirstOrDefault()?.Id;

            StatementResult statementResult = null;

            if (firstAccountId != null)
            {
                var accountId = (Guid) firstAccountId;

                var apiClientForStatement = new FlinksClient(CustomerId, Endpoint);

                var authorizeResponseForStatement = apiClientForStatement.Authorize(institution, userName, password,
                    save, mostRecentCached, withMfaQuestions, requestLanguage, scheduleRefresh, tag);

                if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
                {
                    AnswerMfaQuestion(authorizeResponseForStatement.SecurityChallenges);
                }

                if (authorizeResponseForStatement.RequestId != null)
                {
                    apiClientForStatement.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponseForStatement.RequestId,
                        authorizeResponseForStatement.SecurityChallenges);

                    statementResult = apiClientForStatement.GetStatements(
                        (Guid) authorizeResponseForStatement.RequestId, null, new List<Guid>()
                        {
                            accountId
                        });
                }
            }

            //Assert.Equal(12, statementResult.StatementsByAccount.FirstOrDefault()?.Statements.Count);
            //Assert.NotNull(statementResult.StatementsByAccount);
            //Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            //Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_schedule_refresh_of_an_account(string institution, string userName, string password,
            bool? save, bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage,
            bool? scheduleRefresh, string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }


            if (authorizeResponse.RequestId != null)
            {
                var answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                var loginId = new Guid(answerMfaQuestionsAndAuthorizeResult.Login.Id);

                var getAccountsDetailResult = apiClient.SetScheduledRefresh(loginId, true);

                Assert.Contains($"{loginId} has now been activated for automatic refresh", getAccountsDetailResult);
                Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            }

            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_deactivate_schedule_refresh_of_an_account(string institution, string userName,
            string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions,
            RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            var scheduledRefreshResult = string.Empty;
            var loginId = Guid.Empty;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                loginId = new Guid(answerMfaQuestionsAndAuthorizeResult.Login.Id);

                scheduledRefreshResult = apiClient.SetScheduledRefresh(loginId, false);
            }

            Assert.Contains($"{loginId} has now been deactivated for automatic refresh", scheduledRefreshResult);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_delete_a_card(string institution, string userName, string password, bool? save,
            bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh,
            string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, save, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;
            var loginId = Guid.Empty;
            DeleteCardResult deleteCardResult = null;
            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                apiClient.GetAccountDetails((Guid) authorizeResponse.RequestId, null, null, null, null, null, null);

                loginId = new Guid(answerMfaQuestionsAndAuthorizeResult.Login.Id);

                deleteCardResult = apiClient.DeleteCard(loginId);
            }

            Assert.Equal($"All of the information about LoginId {loginId} has been removed", deleteCardResult?.Message);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult?.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_get_a_score(string institution, string userName, string password, bool? save,
            bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh,
            string tag = null)
        {
            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, true, mostRecentCached,
                withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            ScoreResult scoreResult = null;
            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult =
                    apiClient.AnswerMfaQuestionsAndAuthorize((Guid) authorizeResponse.RequestId,
                        authorizeResponse.SecurityChallenges);

                apiClient.GetAccountDetails((Guid)authorizeResponse.RequestId, null, null, null, null, null, null);

                var authorizeResponseInCachedMode = apiClient.Authorize(institution, userName, password, null, true,
                    withMfaQuestions, requestLanguage, scheduleRefresh, tag);

                var loginId = new Guid(authorizeResponseInCachedMode.Login.Id);

                var scoreRequestBody = new ScoreRequestBody()
                {
                    LoanAmount = "1000.00",
                    UserPersonalInformation = new UserPersonalInformation()
                    {
                        FirstName = "Nicolas",
                        LastName = "Jourdain",
                        Sex = Sex.Male,
                        BirthDate = "1988-08-14",
                        Email = "nicolas.jourdain2@gmail.com",
                        SocialInsuranceNumber = "123456789",
                        Address = new Address()
                        {
                            CivicAddress = "123 fake street",
                            City = "Montreal",
                            Province = "Quebec",
                            PostalCode = "H0H0H0",
                            Country = "Canada",
                            PoBox = ""
                        }
                    }
                };

                if (authorizeResponseInCachedMode.RequestId != null)
                {
                    scoreResult = apiClient.GetScore(loginId, (Guid) authorizeResponseInCachedMode.RequestId,
                        scoreRequestBody);
                }
            }

            Assert.NotNull(scoreResult?.Score);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

        [Theory]
        [MemberData(nameof(AuthorizeTest.TestData), MemberType = typeof(AuthorizeTest))]
        public void Should_get_a_attributes(string institution, string userName, string password, bool? save, bool? mostRecentCached, bool? withMfaQuestions, RequestLanguage? requestLanguage, bool? scheduleRefresh, string tag = null)
        {
            if (save != null && save == false) return;

            var apiClient = new FlinksClient(CustomerId, Endpoint);

            var authorizeResponse = apiClient.Authorize(institution, userName, password, true, mostRecentCached, withMfaQuestions, requestLanguage, scheduleRefresh, tag);

            if (authorizeResponse.ClientStatus == ClientStatus.PENDING_MFA_ANSWERS)
            {
                AnswerMfaQuestion(authorizeResponse.SecurityChallenges);
            }

            AttributeResult attributeResult = null;
            AuthorizeResult answerMfaQuestionsAndAuthorizeResult = null;

            if (authorizeResponse.RequestId != null)
            {
                answerMfaQuestionsAndAuthorizeResult = apiClient.AnswerMfaQuestionsAndAuthorize((Guid)authorizeResponse.RequestId, authorizeResponse.SecurityChallenges);

                apiClient.GetAccountDetails((Guid)authorizeResponse.RequestId, null, null, null, null, null, null);

                var authorizeResponseInCachedMode = apiClient.Authorize(institution, userName, password, null, true, withMfaQuestions, requestLanguage, scheduleRefresh, tag);

                var loginId = new Guid(authorizeResponseInCachedMode.Login.Id);

                var attributesRequestBody = new AttributeRequestBody()
                {
                    Attributes =new Attributes()
                    {
                        Cards = new List<string>()
                        {
                            "account_age",
                            "balance_current",
                            "balance_max",
                            "balance_min",
                            "nbr_nsf",
                            "nbr_overdraft",
                            "nbr_deposits_over500",
                            "nbr_withdrawals_over500",
                            "avg_monthly_deposit"
                        }
                    },
                    Filters = new Filters()
                    {
                        AccountCategories = new List<string>()
                        {
                            AccountCategoryConstant.Operations
                        }
                    }
                };

                if (authorizeResponseInCachedMode.RequestId != null)
                {
                    attributeResult = apiClient.GetAttributes(loginId, (Guid)authorizeResponseInCachedMode.RequestId, attributesRequestBody);
                }
            }

            Assert.NotNull(attributeResult?.Card);
            Assert.Equal(200, answerMfaQuestionsAndAuthorizeResult.HttpStatusCode);
            Assert.Equal(ClientStatus.AUTHORIZED, apiClient.ClientStatus);
        }

    }
}

