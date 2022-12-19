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
    public class DuesManager : IDuesService
    {
        private readonly IDuesDAL _duesDal;

        public DuesManager(IDuesDAL duesDal)
        {
            _duesDal = duesDal;
        }

        public void TAdd(Dues t)
        {
            _duesDal.Add(t);
        }

        public void TDelete(Dues t)
        {
            _duesDal.Delete(t);
        }

        public Dues TGetByID(int id)
        {
            return _duesDal.GetByID(id);    
        }

        public List<Dues> TGetList()
        {
            return _duesDal.GetList();
        }

        public void TUpdate(Dues t)
        {
            _duesDal.Update(t);
        }
    }
}
