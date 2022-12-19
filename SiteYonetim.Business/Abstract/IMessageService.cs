using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Business.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        List<Message> TGetlistInbox();
        List<Message> TGetlistSendbox();

    }
}
