// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Flinks.CSharp.SDK.Model.Shared;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Authorize
{
    public class AuthTokenResult : FlinksRoot
    {
        [JsonProperty("FlinksCode")]
        public string FlinksCode { get; set; }
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
