using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopPlatform.Models.Account
{
    public class User
    {
        public string VendorName
        {
            get
            {
                return vendorName;
            }
            set
            {
                vendorName = value;
            }
        }

        public AccountType AccountType
        {
            get
            {
                return accountType;
            }
            set
            {
                accountType = value;
            }
        }

        public string PhotoUrl
        {
            get
            {
                return photoUrl;
            }
            set
            {
                photoUrl = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
            }
        }

        private string vendorName;
        private AccountType accountType;
        private string photoUrl;
        private string email;
        private string firstName;
        private string secondName;
    }
    public enum AccountType
    {
        Merchant,
        Employee,
        Customer
    }
}
