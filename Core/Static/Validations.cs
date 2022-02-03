namespace Core.Static
{
    public class Validations
    {
        public static bool IsInvalidId(int Id)
            => Id == 0;

        public static bool ValidateLongitude(double longitude)
             => (longitude < -180 || longitude > 180);

        public static bool ValidateLatitude(double latitude)
             => (latitude < -90 || latitude > 90);
    }
}
