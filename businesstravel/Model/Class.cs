using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace businesstravel.Model
{
    public class LoginRequest
    {
        public string UserId { get; set; }
        public string HashedPassword { get; set; }
        public string Company { get; set; }
    }

    //public class LoginResponse
    //{
    //    public LoginResponse()
    //    {

    //        this.Token = "";
    //        this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
    //    }

    //    public string Token { get; set; }
    //    public HttpResponseMessage responseMsg { get; set; }

    //}

    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}