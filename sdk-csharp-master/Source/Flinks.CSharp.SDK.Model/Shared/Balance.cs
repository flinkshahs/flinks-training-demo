// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class Balance
    {
        [JsonProperty("Available")]
        public double? Available { get; set; }

        [JsonProperty("Current")]
        public double? Current { get; set; }

        [JsonProperty("Limit")]
        public double? Limit { get; set; }
    }
}
