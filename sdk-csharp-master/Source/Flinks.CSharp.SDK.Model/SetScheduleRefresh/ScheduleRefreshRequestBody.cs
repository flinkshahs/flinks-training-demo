// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.SetScheduleRefresh
{
    public class ScheduleRefreshRequestBody
    {
        [JsonProperty("LoginId")]
        public string LoginId { get; set; }
        [JsonProperty("IsActivated")]
        public string IsActivated { get; set; }
    }
}
