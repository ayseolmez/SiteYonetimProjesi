﻿using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Business.Abstract
{
    public interface IBillsService : IGenericService<Bills>
    {
        int CalculateBill(int RoomCount);
    }
}
