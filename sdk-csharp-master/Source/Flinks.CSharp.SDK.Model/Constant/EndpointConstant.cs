// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace Flinks.CSharp.SDK.Model.Constant
{
    public static class EndpointConstant
    {
        public const string BaseUrl = "{Instance}/v3/{CustomerId}";
        public const string Authorize = "BankingServices/Authorize";
        public const string GetAccountsSummary = "BankingServices/GetAccountsSummary";
        public const string GetAccountsDetail = "BankingServices/GetAccountsDetail ";
        public const string GetAccountsDetailAsync = "BankingServices/GetAccountsDetailAsync";
        public const string GetStatements = "BankingServices/GetStatements";
        public const string GetStatementsAsync = "BankingServices/GetStatementsAsync";
        public const string SetScheduledRefresh = "BankingServices/SetScheduledRefresh";
        public const string DeleteCard = "BankingServices/DeleteCard";
        public const string GetScore = "Insight/login/{LoginId}/score/{RequestId}";
        public const string GetAttribute = "Insight/login/{LoginId}/attributes/{RequestId}";
        public const string GenerateAuthorizeToken = "BankingServices/GenerateAuthorizeToken";
    }
}
