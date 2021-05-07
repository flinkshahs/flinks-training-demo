// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.GetStatement
{
    public class Statement
    {
        [JsonProperty("UniqueId")]
        public string UniqueId { get; set; }

        [JsonProperty("FileType")]
        public string FileType { get; set; }

        [JsonProperty("Base64Bytes")]
        public string Base64Bytes { get; set; }
    }
}
