// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Shared
{
    public class Account
    {
        [JsonProperty("EftEligibleRatio")]
        public decimal EftEligibleRatio { get; set; }

        [JsonProperty("Transactions")]
        public List<Transaction> Transactions { get; set; }

        [JsonProperty("TransitNumber")]
        public int? TransitNumber { get; set; }

        [JsonProperty("InstitutionNumber")]
        public int? InstitutionNumber { get; set; }

        [JsonProperty("OverdraftLimit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OverdraftLimit { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("Balance")]
        public Balance Balance { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Holder")]
        public Holder Holder { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }
    }
}
