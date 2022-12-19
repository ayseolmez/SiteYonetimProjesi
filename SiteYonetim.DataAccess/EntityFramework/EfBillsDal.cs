using SiteYonetim.DataAccess.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.DataAccess.Repository;
using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.DataAccess.EntityFramework
{
    public class EfBillsDal : GenericRepository<Bills>, IBillsDAL
    {
        public EfBillsDal(SiteManagementDbContext context) : base(context)
        {
        }

        public int CalculateBill(int RoomCount)
        {
            int price = 0;

            if (RoomCount < 0 && RoomCount > 6)
            {
                price = 0;
            }
            if (RoomCount == 1)
            {
                price = 200;
            }
            if (RoomCount == 2)
            {
                price = 300;
            }
            if (RoomCount > 2)
            {
                price = 500;
            }

            return price;
        }


    }
}
