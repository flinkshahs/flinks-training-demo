// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Authorize
{
    public class SecurityChallenge
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Prompt")]
        public string Prompt { get; set; }
        [JsonIgnore]
        public string Answer { get; set; }
    }
}
