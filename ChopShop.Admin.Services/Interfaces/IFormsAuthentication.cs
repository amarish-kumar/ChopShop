using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IFormsAuthentication
    {
        void SignOut();
        void SetAuthCookie(string userName, bool createPersistentCookie);
    }
}
