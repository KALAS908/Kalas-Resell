using OnlineStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.Account.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            CountryList = new List<Country>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public int CountryId { get; set; }

        public List<Country> CountryList { get; set; }
    }
}
