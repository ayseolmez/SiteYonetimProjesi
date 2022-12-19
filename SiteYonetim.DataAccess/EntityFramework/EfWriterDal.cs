using Microsoft.AspNetCore.Hosting;
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
	public class EfWriterDal : GenericRepository<Writer>, IWriterDAL
	{
		public EfWriterDal(SiteManagementDbContext context) : base(context)
		{
            _context = context;
        }

        private readonly SiteManagementDbContext _context;
    }
}
