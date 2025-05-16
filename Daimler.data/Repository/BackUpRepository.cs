using Microsoft.EntityFrameworkCore;
using Daimler.data.IRepository;
using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Repository
{
    public class BackUpRepository : IBackUpRepository
    {
        public readonly DAIMLERContext _context;

        public BackUpRepository(DAIMLERContext context)
        {
            _context = context;
        }

        public void BackUp()
        {
            _context.Database.ExecuteSqlRaw("EXEC [dbo].[BackUpSERTHIDB]");
        }
    }
}
