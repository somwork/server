﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;

namespace TaskHouseApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private ITokenService tokenService;
        private IAuthService authService;
        private IUnitOfWork unitOfWork;

        public TokenController(ITokenService tokenService, IAuthService authService, IUnitOfWork unitOfWork)
        {
            this.tokenService = tokenService;
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult Create([FromBody]LoginModel login)
        {
            if (!TryValidateModel(login))
            {
                return BadRequest(new { error = "Model not valid" });
            }

            // Check's if user is autherised
            User user = authService.Authenticate(login);
            if (user == null)
            {
                return Unauthorized();
            }

            // Sets the claims for the token
            var usersClaims = new[]
            {
                // User Id
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.GetType().ToString())
            };

            string tokenString = tokenService.GenerateAccessToken(usersClaims);
            string refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add
            (
                new RefreshToken { Token = refreshToken }
            );

            unitOfWork.Users.Update(user);
            unitOfWork.Save();


            return new ObjectResult(new
            {
                accessToken = tokenString,
                refreshToken = refreshToken
            });
        }

        [HttpPut]
        public IActionResult Refresh([FromBody]RefreshModel refreshModel)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(refreshModel.AccessToken);

            // If null, the token isn't valid
            if (principal == null)
            {
                return BadRequest();
            }

            // Gets the user Id from Claims
            int Id;
            Int32.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out Id);

            User user = unitOfWork.Users.Retrieve(Id);

            // Checks that the user exists and has a refresh token
            if (user == null || user.RefreshTokens.Count() == 0)
            {
                return BadRequest();
            }

            // Checks that the refresh token from the client,
            // matches one asignt to the user
            RefreshToken storedRefreshToken = null;
            foreach (RefreshToken r in user.RefreshTokens)
            {
                if ((r.Token).Equals(refreshModel.RefreshToken))
                {
                    storedRefreshToken = r;
                }
            }

            if (storedRefreshToken == null)
            {
                return BadRequest();
            }

            // Generates a new access token and refresh token
            var newJwtToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            // Deletes the old refresh token from database
            unitOfWork.Users.DeleteRefrechToken(storedRefreshToken);

            // Add the new refresh token to user
            user.RefreshTokens.Add
            (
                new RefreshToken { Token = newRefreshToken }
            );

            unitOfWork.Users.Update(user);
            unitOfWork.Save();

            return new ObjectResult(new
            {
                accessToken = newJwtToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
