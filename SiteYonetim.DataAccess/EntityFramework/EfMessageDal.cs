using Microsoft.EntityFrameworkCore;
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
    public class EfMessageDal : GenericRepository<Message>, IMessageDAL
    {

        public EfMessageDal(SiteManagementDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly SiteManagementDbContext _context;

        public List<Message> GetlistInbox()
        {
            
            
                return _context.Messages.Where(x => x.ReceiverMail == "admin@gmail.com").ToList();
            
        }

        public List<Message> GetlistSendbox()
        {
           
            
                return _context.Messages.Where(x => x.SenderMail == "admin@gmail.com").ToList();
            
        }

        
    }
}
