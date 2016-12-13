using System.Collections.Generic;
using UserApp.CommonFiles.DTO;

namespace UserApp.InfrastructureInterfaces
{
    public interface IDefaultService<DTOType> where DTOType:DTO
    {
        void Delete(int id);
        void Edit(DTOType dto);
        DTOType Get(int id);
        IEnumerable<DTOType> GetAll();
        bool Has(int id);
        void Insert(DTOType dto);
    }
}