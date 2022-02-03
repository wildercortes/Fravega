using Data.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class BranchOfficeRepository : IBranchOfficeRepository
    {
        private readonly DataContext dataContext;

        public BranchOfficeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Add(BranchOffice branchOffice)
        {
            dataContext.Sucursales.Add(branchOffice);
            dataContext.SaveChanges();

        }

        public BranchOffice GetBranchOfficeById(int id)
            => dataContext.Sucursales.Where(x => x.Id == id).FirstOrDefault();

        public List<BranchOffice> GetAllBranchOffice()
            => dataContext.Sucursales.ToList();

        public int CountAllBranchOffice()
           => GetAllBranchOffice().Count();


    }
}
