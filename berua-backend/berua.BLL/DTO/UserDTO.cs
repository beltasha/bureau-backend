using System;
using System.Collections.Generic;
using System.Text;

namespace berua.BLL.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
