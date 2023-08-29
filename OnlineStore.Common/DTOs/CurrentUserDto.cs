using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineStore.Common.DTOs
{
    public class CurrentUserDto
    {
        public CurrentUserDto()
        {
           
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string CountryId { get; set; }

    }
}
