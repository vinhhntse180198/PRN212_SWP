using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class ADNService
    {
        private ServiceRepository service = new();

        public List<Service> GetAllService()
        {
            return service.GetAll();
        }

        public List<Service> GetBySearch(string key)
        {
            return service.GetBySearch(key);
        }

        public void Create(Service x)
        {
            service.Create(x);
        }

        public void Update(Service x)
        {
            service.Update(x);
        }

        public void Delete(Service x)
        {
            service.Delete(x);
        }
    }
}
