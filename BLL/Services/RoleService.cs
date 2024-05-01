using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {


        public RoleManager<Role> _RoleManager;
        public UserManager<User> _UserManager;
        private readonly AppDBContext dbContext;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDBContext dbContext)
        {
            _RoleManager = roleManager;
            _UserManager = userManager;
            this.dbContext = dbContext;
        }
        public async Task AsignRole(Guid id, string roleName)
        {
            try
            {
                var user = await _UserManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var role = await _RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    // Якщо роль не існує, створити її
                    role = new Role(roleName);
                    await _RoleManager.CreateAsync(role);
                }

                // Присвоїти роль користувачеві
                await _UserManager.AddToRoleAsync(user, roleName);
            }
            catch (ObjectDisposedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}
