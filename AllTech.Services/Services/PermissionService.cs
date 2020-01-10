using AllTech.DataLayer.Context;
using AllTech.DomainClasses.User;
using AllTech.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AllTechDbContext _db;
        public PermissionService(AllTechDbContext db)
        {
            _db = db;
        }

        #region Role

        public List<Roles> GetRoles()
        {
           
            return Enum.GetValues(typeof(Roles)).Cast<Roles>().ToList();
        }
        public string GetRoleName(Roles roleId)
        {
            byte i = (byte)roleId;
            switch (i)
            {
                case 1:
                    return "مدیر سایت";
                case 2:
                    return "نویسنده";
                case 4:
                    return "کاربر ویژه";
                case 8:
                    return "کاربر عادی";
                default:
                    return "ERROR 404";
            }
        }
        public void AddRolesToUser(List<Roles> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _db.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }
            _db.SaveChanges();
        }

        public void EditUserRoles(int userId, List<Roles> rolesId)
        {
            //Delete All User Roles
            _db.UserRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _db.UserRoles.Remove(r));
            AddRolesToUser( rolesId, userId);
        }

       
        #endregion

    }
}
