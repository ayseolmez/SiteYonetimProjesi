using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Abstract;
using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Business.Concrete
{
    public class ApartmentManager : IApartmentService
    {
        private readonly IApartmentDAL _apartmentDal;

        public ApartmentManager(IApartmentDAL apartmentDal)
        {
            _apartmentDal = apartmentDal;
        }

        public void TAdd(Apartment t)
        {
            _apartmentDal.Add(t);
        }

        public void TDelete(Apartment t)
        {
            _apartmentDal.Delete(t);
        }

        public Apartment TGetByID(int id)
        {
            return _apartmentDal.GetByID(id);
        }

        public List<Apartment> TGetList()
        {
            return _apartmentDal.GetList();
        }

        public void TUpdate(Apartment t)
        {
            _apartmentDal.Update(t);
        }
    }
}
