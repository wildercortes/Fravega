using System;

namespace Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message, Exception innerException, BusinessExceptionCode code = BusinessExceptionCode.NotDefined)
            : base(message, innerException)
        {
            Code = code;
        }

        public BusinessException(string message, BusinessExceptionCode code = BusinessExceptionCode.NotDefined)
            : base(message)
        {
            Code = code;
        }
        public BusinessExceptionCode Code { get; set; }

    }
}
