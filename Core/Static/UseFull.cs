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

        public static bool IsInvalidLongOrLat(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90) return false;
            if (longitude < -180 || longitude > 180) return false;

            return true;
        }

        public static bool ValidateLongitude(double longitude)
             => (longitude < -180 || longitude > 180);

        public static bool ValidateLatitude(double latitude)
             => (latitude < -90 || latitude > 90);


    }
}
