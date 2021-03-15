using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Application.Configurations.Data
{
    public interface IIdentityAccessDbContext
    {
        Task SaveChangesAsync();
    }
}