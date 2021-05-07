// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class Login
    {
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("IsScheduledRefresh")]
        public string IsScheduledRefresh { get; set; }
        [JsonProperty("LastRefresh")]
        public string LastRefresh { get; set; }
        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
