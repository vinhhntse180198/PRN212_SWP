using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class KitService
    {
        private KitRepository _repository = new KitRepository();

        public List<KitComponent> GetAllKits()
        {
            return _repository.GetAll();
        }

        public List<KitComponent> GetKitsByService(long serviceId)
        {
            return _repository.GetByService(serviceId);
        }

        public List<KitComponent> SearchKits(string searchText)
        {
            return _repository.Search(searchText);
        }

        public void CreateKit(KitComponent kit)
        {
            _repository.Create(kit);
        }

        public void UpdateKit(KitComponent kit)
        {
            _repository.Update(kit);
        }

        public void DeleteKit(KitComponent kit)
        {
            _repository.Delete(kit);
        }
    }
} 