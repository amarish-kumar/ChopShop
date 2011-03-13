using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChopShop.Model;

namespace ChopShop.Admin.Services.Interfaces
{
    public interface IAdminService
    {
        AdminUser GetUserForLogin(string email, string password);
    }
}
