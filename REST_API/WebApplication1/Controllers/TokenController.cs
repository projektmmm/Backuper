﻿using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;
using System.IdentityModel.Tokens.Jwt;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TokenController : ApiController
    {
        [Route("api/admin/{token}")]
        public string Get(string intoken)
        {
            Token token = JsonConvert.DeserializeObject<Token>(intoken);
            string ret = (token.Verify(token.IdAdmin, token.Password)) ? "true" : "false";
            return ret;
        }
    }
}