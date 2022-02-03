namespace Core.Exceptions
{
    public enum BusinessExceptionCode
    {
        NotDefined,
        RequireId,
        LatInvalid,
        LongInvalid,
        AddressRequired,
        LongOrLatInvalid,
        LongitudeOutRange,
        LatitudeOutRange,
        BranchOfficeNotExist,
        BranchOfficeCreationError
    }
}
