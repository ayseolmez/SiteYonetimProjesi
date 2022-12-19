using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteYonetim.Business.Abstract;
using SiteYonetim.Business.Concrete;
using SiteYonetim.DataAccess.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Business.IoC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            

         

            services.AddScoped<IApartmentService, ApartmentManager>();
            services.AddScoped<IBillsService, BillsManager>();
            services.AddScoped<IDuesService, DuesManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IWriterService, WriterService>();

            services.AddScoped<IApartmentDAL, EfApartmentDal>();
            services.AddScoped<IBillsDAL, EfBillsDal>();
            services.AddScoped<IDuesDAL, EfDuesDal>();
            services.AddScoped<IMessageDAL, EfMessageDal>();
            services.AddScoped<IWriterDAL, EfWriterDal>();


        }
    }
}
