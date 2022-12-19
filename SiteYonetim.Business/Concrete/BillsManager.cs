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
    public class BillsManager : IBillsService
    {
        private readonly IBillsDAL _billsDal;

        public BillsManager(IBillsDAL billsDal)
        {
            _billsDal = billsDal;
        }

        public int CalculateBill(int RoomCount)
        {
            return _billsDal.CalculateBill(RoomCount);
        }

        public void TAdd(Bills t)
        {
            _billsDal.Add(t);
        }

        public void TDelete(Bills t)
        {
            _billsDal.Delete(t);
        }

        public Bills TGetByID(int id)
        {
            return _billsDal.GetByID(id);
        }

        public List<Bills> TGetList()
        {
            return _billsDal.GetList();
        }

        public void TUpdate(Bills t)
        {
            _billsDal.Update(t);
        }
    }
}
