using Core.Request;
using Data.DTOs;
using Entities;

namespace Core.Static
{
    public class UseFull
    {
        public static BranchOffice BecomeRequestIntoEntity(AddBranchOfficeRequest request)
          => new BranchOffice
          {
              Direccion = request.Direccion,
              Longitud = request.Longitud,
              Latitud = request.Latitud
          };


        public static BranchOfficeDTO BecomeEntityIntoDTO(BranchOffice Entity)
           => new BranchOfficeDTO
           {
               Direccion = Entity.Direccion,
               Longitud = Entity.Longitud,
               Latitud = Entity.Latitud
           };

        public static bool IsInvalidId(int Id)
            => Id == 0;

        public static bool IsInvalidLongOrLat(double Longitud, double Latitude)
           => ((Longitud == 0 && Latitude == 0) || (Longitud == 0 || Latitude == 0));


    }
}
