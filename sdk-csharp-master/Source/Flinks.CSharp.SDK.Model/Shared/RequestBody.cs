// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class RequestBody : FlinksRoot
    {
        [JsonIgnore]
        public bool IsAuthenticated => !string.IsNullOrEmpty(LoginId) && RequestId != null;

        [JsonProperty("LoginId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LoginId { get; set; }
        [JsonProperty("DaysOfTransactions", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DaysOfTransactions { get; set; }
    }
}
