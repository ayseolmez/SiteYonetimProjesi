using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.DataAccess.Abstract
{
    public interface IMessageDAL : IGenericDAL<Message>
    {
        List<Message> GetlistInbox();
        List<Message> GetlistSendbox();
        


    }
}
