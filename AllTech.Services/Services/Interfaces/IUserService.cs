using AllTech.DomainClasses.User;
using AllTech.DomainClasses.Wallet;
using AllTech.ViewModels.UserAccount;
using AllTech.ViewModels.Wallet;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllTech.Services.Services.Interfaces
{
    public interface IUserService
    {
        #region UserAccount
        bool IsUsernameExist(string userName);
        bool IsEmailExist(string email);
        int AddUser(User user);
        User LoginUser(LoginViewModel login);
        bool ActiveAccount(string activeCode);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);
        User GetUserByUserName(string userName);
        User GetUserByUserId(string userId);
        User GetUserByUserIdInt(int userId);
        User GetUserByEmailForExternalLogin(string email);
        int GetUserIdIntValue(string userId);
        int GetUserIdByUserName(string userName);
        #endregion

        #region UserPanel

        UserInformationViewModel GetUserInformation(string userId);
        UserPanelSideBarViewModel GetSideBarInformation(string userId);
        EditProfileViewModel GetUserInformationToEdit(string userId);
        void EditProfile(string userId, EditProfileViewModel editProfile);
        bool CompareOldPassword(string userId, string oldPassword);
        void ChangePassword(string userId, string newPassword);
        #endregion

        #region Wallet

        int BalanceUserWallet(string userId);
        List<WalletViewModel> GetUserWallet(string userId);
        int ChargeWallet(string userId,int amount,string description,bool isPay=false);
        int AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        void UpdateWallet(Wallet wallet);
        #endregion

        #region Admin

        UsersForAdminViewModel GetUsers(int pageId=1, string filterUserName="", string filterEmail="");
        int AddUserFromAdmin(CreateUserViewModel user);
        EditUserViewModel GetUserForEditByAdmin(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
        #endregion
    }
}
