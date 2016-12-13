using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.CommonFiles.DTO;

namespace UserApp.InfrastructureInterfaces
{
    public interface IUserService:IDefaultService<UserDTO>
    {
    }
}
