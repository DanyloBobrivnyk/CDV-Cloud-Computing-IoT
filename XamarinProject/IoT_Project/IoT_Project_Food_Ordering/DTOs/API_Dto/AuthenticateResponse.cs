using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Project_Food_Ordering.DTOs.API_Dto
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

    }
}
