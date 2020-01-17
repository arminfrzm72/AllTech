using AllTech.DomainClasses.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.Services.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Roles> GetRoles();
        string GetRoleName(Roles roleId);
        void AddRolesToUser(List<Roles> roleIds, int userId);
        void EditUserRoles(int userId, List<Roles> rolesId);
        bool CheckPermission(int roleId, string userName);
    }
}
