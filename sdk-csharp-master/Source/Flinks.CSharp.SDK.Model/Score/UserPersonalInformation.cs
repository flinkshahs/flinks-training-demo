// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using Flinks.CSharp.SDK.Model.Enums;
using Flinks.CSharp.SDK.Model.Shared;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Score
{
    public class UserPersonalInformation
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Sex")]
        public Sex Sex { get; set; }

        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("SocialInsuranceNumber")]
        public string SocialInsuranceNumber { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }
    }
}
