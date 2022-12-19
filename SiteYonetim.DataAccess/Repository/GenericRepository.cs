using Microsoft.EntityFrameworkCore;
using SiteYonetim.DataAccess.Abstract;
using SiteYonetim.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        private readonly SiteManagementDbContext _context;

        public GenericRepository(SiteManagementDbContext context)
        {
            _context = context;
        }

        public void Add(T t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();

        }

        public void Update(T t)
        {
            _context.Entry<T>(t).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
