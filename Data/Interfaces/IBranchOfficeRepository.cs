using Entities;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IBranchOfficeRepository
    {
        void Add(BranchOffice branchOffice);
        int CountAllBranchOffice();
        List<BranchOffice> GetAllBranchOffice();
        BranchOffice GetBranchOfficeById(int id);
    }
}