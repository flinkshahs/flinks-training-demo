// - This Source Code Form is subject to the terms of the Mozilla Public
// - License, v. 2.0. If a copy of the MPL was not distributed with this
// - file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace Flinks.CSharp.SDK.Model.Shared
{
    public enum ClientStatus
    {
        UNKNOWN = 0,
        ERROR = 1,
        PENDING_MFA_ANSWERS = 2,
        AUTHORIZED = 3,
        UNAUTHORIZED = 4
    }
}
