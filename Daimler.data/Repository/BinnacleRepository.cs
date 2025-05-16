using Daimler.data.IRepository;
using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Repository
{
    public class BinnacleRepository : BaseRepository<Binnacle>, IBinnacleRepository
    {
        public BinnacleRepository(DAIMLERContext context) : base(context) { }
    }
}
