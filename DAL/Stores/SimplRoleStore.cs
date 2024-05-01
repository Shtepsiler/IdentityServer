using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;

namespace DAL.Stores
{
    public class SimplRoleStore : RoleStore<Role, AppDBContext, Guid, IdentityUserRole<Guid>, IdentityRoleClaim<Guid>>
    {
        public SimplRoleStore(AppDBContext context) : base(context)
        {
        }
    }
}

