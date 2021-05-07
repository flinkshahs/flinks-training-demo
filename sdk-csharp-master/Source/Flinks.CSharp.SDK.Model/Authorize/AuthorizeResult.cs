// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Collections.Generic;
using Flinks.CSharp.SDK.Model.Shared;
using Newtonsoft.Json;

namespace Flinks.CSharp.SDK.Model.Authorize
{
    public class AuthorizeResult : FlinksRoot
    {
        public AuthorizeResult()
        {
            RequestId = null;
        }

        [JsonProperty("SecurityChallenges")]
        public List<SecurityChallenge> SecurityChallenges { get; set; }
        [JsonProperty("Login")]
        public Login Login { get; set; }
    }
}
