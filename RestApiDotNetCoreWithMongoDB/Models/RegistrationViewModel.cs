using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDotNetCoreWithMongoDB.Models
{
    public class RegistrationViewModel
    {
        public string Password { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }


    }
}
