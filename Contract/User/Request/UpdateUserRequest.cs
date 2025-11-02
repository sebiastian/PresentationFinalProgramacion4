using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.User.Request;

public class UpdateUserRequest
{
    

       
        public string Name { get; set; } = null!;

        
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }
     

}
