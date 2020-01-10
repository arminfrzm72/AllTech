using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllTech.ViewModels.Wallet
{

    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }
    public class WalletViewModel
    {
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}
