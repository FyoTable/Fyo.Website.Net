using Fyo.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fyo.Models {
    public class User : BaseModel {
        
        //currently this is auth0 and comes in the format auth0|<userid>
        public string ThirdPartyUserId { get; set; }

        public UserRole UserRole { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
    }
}