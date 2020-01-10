using AllTech.DataLayer.Context;
using AllTech.DomainClasses.User;
using AllTech.DomainClasses.Wallet;
using AllTech.Services.Services.Interfaces;
using AllTech.Utilities.Generator;
using AllTech.Utilities.Security;
using AllTech.ViewModels.UserAccount;
using AllTech.ViewModels.Wallet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TopLearn.Core.Utilities;

namespace AllTech.Services.Services
{
    public class UserService : IUserService
    {
        private readonly AllTechDbContext _db;
        public UserService(AllTechDbContext db)
        {
            _db = db;
        }

        #region UserAccount

        public bool ActiveAccount(string activeCode)
        {
            var user = _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqueCode();
            _db.SaveChanges();
            return true;
        }

        public int AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user.UserID;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByEmailForExternalLogin(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUserName(string userName)
        {
            return _db.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public bool IsEmailExist(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameExist(string userName)
        {
            return _db.Users.Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            var hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            var userName = login.UserName;
            return _db.Users.SingleOrDefault(u => u.UserName == userName && u.Password == hashPassword);
        }

        public void UpdateUser(User user)
        {
            _db.Update(user);
            _db.SaveChanges();           
        }


        public User GetUserByUserId(string userId)
        {
            return _db.Users.SingleOrDefault(u => u.UserID.ToString() == userId);
        }

        public User GetUserByUserIdInt(int userId)
        {
            return _db.Users.SingleOrDefault(u => u.UserID == userId);
        }

        public int GetUserIdIntValue(string userId)
        {
            return _db.Users.Single(u => u.UserID.ToString() == userId).UserID;
        }
        #endregion

        #region UserPanel

        public UserInformationViewModel GetUserInformation(string userId)
        {
            var user = GetUserByUserId(userId);
            UserInformationViewModel information = new UserInformationViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                WalletBalance = BalanceUserWallet(userId)

            };
            return information;
        }

        public UserPanelSideBarViewModel GetSideBarInformation(string userId)
        {
            return _db.Users.Where(u => u.UserID.ToString() == userId).Select(u => new UserPanelSideBarViewModel
            {
                UserName = u.UserName,
                ImageName = u.UserAvatar
            }).Single();
        }

        public EditProfileViewModel GetUserInformationToEdit(string userId)
        {
            return _db.Users.Where(u => u.UserID.ToString() == userId).Select(u => new EditProfileViewModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                AvatarName = u.UserAvatar

            }).Single();
        }

        public void EditProfile(string userId, EditProfileViewModel editProfile)
        {
            if (editProfile.UserAvatar != null)
            {
                if (FormFileExtensions.IsImage(editProfile.UserAvatar))
                {
                    string imagePath;
                    if (editProfile.AvatarName != "Default.jpg")
                    {
                        imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", editProfile.AvatarName);
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                    }
                    editProfile.AvatarName = NameGenerator.GenerateUniqueCode() + Path.GetExtension(editProfile.UserAvatar.FileName);
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatars", editProfile.AvatarName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        editProfile.UserAvatar.CopyToAsync(stream);
                    }
                }
            }
            var user = GetUserByUserId(userId);
            user.FirstName = editProfile.FirstName;
            user.LastName = editProfile.LastName;
            user.UserName = editProfile.UserName;
            user.Email = editProfile.Email;
            if (editProfile.UserAvatar != null)
            {
                user.UserAvatar = editProfile.AvatarName;
            }

            UpdateUser(user);
        }

        public bool CompareOldPassword(string userId, string oldPassword)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _db.Users.Any(u => u.UserID.ToString() == userId && u.Password == hashOldPassword);

        }

        public void ChangePassword(string userId, string newPassword)
        {
            var user = GetUserByUserId(userId);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }



        #endregion

        #region Wallet

        public int BalanceUserWallet(string userId)
        {
            var deposite = _db.Wallets.Where(w => w.UserID.ToString() == userId && w.TypeId == 1 && w.IsPay == true)
                .Select(w => w.Amount).ToList();
            var removal = _db.Wallets.Where(w => w.UserID.ToString() == userId && w.TypeId == 2)
                .Select(w => w.Amount).ToList();
            return (deposite.Sum() - removal.Sum());
        }

        public List<WalletViewModel> GetUserWallet(string userId)
        {
            return _db.Wallets.Where(w => w.UserID.ToString() == userId && w.IsPay).Select(w => new WalletViewModel
            {
                Amount = w.Amount,
                TransactionDate = w.PayDate,
                Type = w.TypeId,
                Description = w.Description
            }).ToList();
        }

        public int ChargeWallet(string userId, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                TypeId = 1,
                UserID = GetUserIdIntValue(userId),
                Amount = amount,
                Description = description,
                IsPay = isPay,
                PayDate = DateTime.Now
            };
            return AddWallet(wallet);
        }

        public int AddWallet(Wallet wallet)
        {
            _db.Wallets.Add(wallet);
            _db.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWalletByWalletId(int walletId)
        {
            return _db.Wallets.Find(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _db.Wallets.Update(wallet);
            _db.SaveChanges();
        }

        #endregion

        #region Admin

        public UsersForAdminViewModel GetUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            IQueryable<User> result = _db.Users;
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName) || u.UserName == filterUserName);
            }
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail) || u.Email == filterEmail);
            }

            //pages
            byte take = 3;
            int skip = (pageId - 1) * take;

            UsersForAdminViewModel list = new UsersForAdminViewModel
            {
                Users = result.OrderBy(u => u.UserName).Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling((double)result.Count() / take),
                PreviousPage = pageId - 1,
                NextPage = pageId + 1
            };
            if (list.CurrentPage == 1)
            {
                list.PreviousPage = 1;
            }
            if (list.NextPage >= list.PageCount)
            {
                list.NextPage = list.PageCount;
            }
            return list;
        }

       
        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User
            {
                Password = PasswordHelper.EncodePasswordMd5(user.Password)
            };

            #region Save Avatar
            if (user.UserAvatar != null)
            {
                addUser.UserAvatar = Picture.SavePicture(user.UserAvatar, "wwwroot\\UserAvatars");
            }
            #endregion

            addUser.FirstName = user.FirstName;
            addUser.LastName = user.LastName;
            addUser.ActiveCode = NameGenerator.GenerateUniqueCode();
            addUser.Email = user.Email;
            addUser.IsActive = true;          
            addUser.UserName = user.UserName;
            return AddUser(addUser);

        }

        public EditUserViewModel GetUserForEditByAdmin(int userId)
        {
            return _db.Users.Where(u => u.UserID == userId).Select(u => new EditUserViewModel
            {
                UserId = u.UserID,
                FirstName=u.FirstName,
                LastName=u.LastName,
                UserName=u.UserName,
                Email=u.Email,
                AvatarName=u.UserAvatar,
                UserRoles = u.UserRoles.Select(r => (Roles)r.RoleId).ToList()
            }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserByUserIdInt(editUser.UserId);
            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }
            if (editUser.UserAvatar!=null)
            {
                if (editUser.AvatarName!="Default.jpg")
                {
                    Picture.DeletePicture(editUser.AvatarName, "wwwroot\\UserAvatars");
                }

                user.UserAvatar = Picture.SavePicture(editUser.UserAvatar, "wwwroot\\UserAvatars");
            }
            UpdateUser(user);
        }

        

        #endregion


    }
}
