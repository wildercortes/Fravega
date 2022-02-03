using Entities;

namespace Data.DTOs
{
    public class BranchOfficeDTO
    {
        public BranchOfficeDTO()
        {

        }


        public BranchOfficeDTO(BranchOffice obj)
        {
            Id = obj.Id;
            Direccion = obj.Direccion;
            Longitud = obj.Longitud;
            Latitud = obj.Latitud;
        }

        public int Id { get; set; }
        public string Direccion { get; set; }

        public double Longitud { get; set; }

        public double Latitud { get; set; }

    }
}
