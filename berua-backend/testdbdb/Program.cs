using berua.BLL.Actions;
using berua.BLL.DTO;
using System;

namespace testdbdb
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new UserDTO
            {
                Login = "Жанна",
                Password = "11122233",
                Phone = "9217777777"
            };

            UserAction.RegistrationUser(user);

        }
    }
}
