// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;

namespace Flinks.CSharp.SDK.Model.GetStatement
{
    public class StatementsRequestBody
    {
        public string RequestId { get; set; }
        public string NumberOfStatements { get; set; }
        public List<Guid> AccountsFilter { get; set; }
    }
}
