// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Attributes
{
    public class Filters
    {
        [JsonProperty("accountCategory")]
        public List<string> AccountCategories { get; set; }
    }
}
