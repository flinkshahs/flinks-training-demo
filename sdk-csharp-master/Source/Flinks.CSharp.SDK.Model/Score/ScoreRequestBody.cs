// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Score
{
    public class ScoreRequestBody
    {
        [JsonProperty("LoanAmount")]
        public string LoanAmount { get; set; }

        [JsonProperty("UserPersonalInformation")]
        public UserPersonalInformation UserPersonalInformation { get; set; }
    }
}
