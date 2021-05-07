// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class Transaction
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("Code")]
        public object Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Debit")]
        public double? Debit { get; set; }

        [JsonProperty("Credit")]
        public double? Credit { get; set; }

        [JsonProperty("Balance")]
        public double? Balance { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }
    }
}
