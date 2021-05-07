// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections.Generic;
using Flinks.CSharp.SDK.Model.Shared;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Authorize
{
    public class AuthorizeRequestBody : FlinksRoot
    {
        public AuthorizeRequestBody()
        {
            Save = null;
            RequestId = null;
        }

        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("LoginId")]
        public string LoginId { get; set; }
        [JsonProperty("SecurityResponses")]
        public Dictionary<string, List<string>> SecurityResponses { get; set; }
        [JsonProperty("Save")]
        public string Save { get; set; }
        [JsonProperty("MostRecentCached")]
        public string MostRecentCached { get; set; }
        [JsonProperty("WithMfaQuestions")]
        public string WithMfaQuestions { get; set; }
        [JsonProperty("Language")]
        public string Language { get; set; }
        [JsonProperty("Tag")]
        public string Tag { get; set; }
        [JsonProperty("ScheduleRefresh")]
        public string ScheduleRefresh { get; set; }
    }
}
