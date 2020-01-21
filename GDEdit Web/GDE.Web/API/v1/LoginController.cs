﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GDE.Web.Models;
using GDE.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GDE.Web.API.v1
{
    [Route("api/v1/[controller]"), ApiController, Authorize]  
    public class LoginController : ControllerBase
    {
        private IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous, HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { error = "username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jwt = Request.Headers["Authorization"][0].Split(" ")[1]; // gets rid of Bearer
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            
            //TODO: Check on the permission level here. (* = all permission, user_access = show all users, etc)
            return Ok(userService.GetAll());
        }
    }
}