using DataWebApiCodeCamp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebApiCodeCamp
{
    public class CampIdentityInitializer
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<CampUser> _userManager;

        public CampIdentityInitializer(UserManager<CampUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            var user = await _userManager.FindByNameAsync("shawnwildermuth");

            // add user
            if (user == null)
            {
                if (!(await _roleManager.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "IsAdmin", ClaimValue = "True" });
                    await _roleManager.CreateAsync(role);
                }

                user = new CampUser()
                {
                    UserName = "shawnwildermuth",
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    Email = "shawn@wildermuth.com"
                };

                var userResult = await _userManager.CreateAsync(user, "Password!");
                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                var claimResult = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }
}
