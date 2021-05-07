// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class FlinksRoot
    {
        [JsonIgnore]
        public ClientStatus ClientStatus { get; set; }
        [JsonProperty("HttpStatusCode", NullValueHandling = NullValueHandling.Ignore)]
        public int HttpStatusCode { get; set; }
        [JsonProperty("Institution")]
        public string Institution { get; set; }
        [JsonProperty("RequestId")]
        public Guid? RequestId { get; set; }
        [JsonProperty("Links")]
        public List<Link> Links { get; set; }
        [JsonProperty("ValidationDetails", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<string>> ValidationDetails { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("Tag")]
        public string Tag { get; set; }
    }
}
