using Microsoft.AspNetCore.Identity;
using PetShop.Authorization.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Authorization
{
    public class IdentityInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public IdentityInitializer(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                //if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_ALTURAS).Result)
                //{
                //    var resultado = _roleManager.CreateAsync(
                //        new IdentityRole(Roles.ROLE_API_ALTURAS)).Result;
                //    if (!resultado.Succeeded)
                //    {
                //        throw new Exception(
                //            $"Erro durante a criação da role {Roles.ROLE_API_ALTURAS}.");
                //    }
                //}

                CreateUser(
                    new IdentityUser()
                    {

                        UserName = "batman",
                        Email = "batman@teste.com.br",
                        EmailConfirmed = true
                    }, "AdminAPIAlturas01!");

                CreateUser(
                    new IdentityUser()
                    {
                        UserName = "robin",
                        Email = "robin@teste.com.br",
                        EmailConfirmed = true
                    }, "UsrInvAPIAlturas01!");
            }
        }

        private void CreateUser(
            IdentityUser user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
