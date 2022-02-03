using Api.Controllers;
using Data.Interfaces;
using Entities;
using FakeItEasy;
using Xunit;

namespace Api.Tests
{
    public class BranchOfficeControllerTests
    {
        [Fact]
        public void AddValidNewBranchOffice()
        {
            var data = A.Fake<IBranchOfficeRepository>();
            var oldNumberOfBranchOffice = data.CountAllBranchOffice();
            var controller = new BranchOfficeController(data);
            var sucursal = new BranchOffice { Direccion = "mi direccion", Latitud = 1, Longitud = 2 };

            var result = A.CallTo(() => data.Add(sucursal));

            var currentNumberOfBranchOffice = data.CountAllBranchOffice();

            Assert.NotEqual(oldNumberOfBranchOffice, currentNumberOfBranchOffice);

        }


        [Fact]
        public void AddInValidNewBranchOffice()
        {
            var data = A.Fake<IBranchOfficeRepository>();
            var oldNumberOfBranchOffice = data.CountAllBranchOffice();
            var controller = new BranchOfficeController(data);
            var sucursal = new BranchOffice();

            var result = A.CallTo(() => data.Add(sucursal));

            var currentNumberOfBranchOffice = data.CountAllBranchOffice();

            Assert.Equal(oldNumberOfBranchOffice, currentNumberOfBranchOffice);

        }
    }
}
