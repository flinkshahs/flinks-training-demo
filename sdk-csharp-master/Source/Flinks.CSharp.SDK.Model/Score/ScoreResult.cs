// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Flinks.CSharp.SDK.Model.Shared;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Score
{
    public class ScoreResult : FlinksRoot
    {
        [JsonProperty("Score")]
        public string Score { get; set; }

        [JsonProperty("Login")]
        public Login Login { get; set; }
    }
}
