using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopPlatform.Models.Accounting
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
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

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public Password Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public TokensChain TokensChain
        {
            get
            {
                return tokensChain;
            }
            set
            {
                tokensChain = value;
            }
        }

        public Guid ProfileId
        {
            get
            {
                if (profileId == Guid.Empty)
                {
                    profileId = Guid.NewGuid();
                }

                return profileId;
            }
        }
        private Guid profileId;
        private string vendorName;
        private AccountType accountType;
        private string photoUrl;
        private string email;
        private string firstName;
        private string lastName;
        private Password password;
        private TokensChain tokensChain;
    }
    public enum AccountType
    {
        Merchant = 0,
        Employee = 1,
        Customer = 2
    }
}
