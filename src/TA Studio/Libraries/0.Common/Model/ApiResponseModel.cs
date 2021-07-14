using System.Collections.Generic;

namespace _0.Common.Model
{
    public class ApiResponseModel
    {
    }

    public class ResponseModel
    {
        public bool Status { get; set; }
        public List<string> LocaleParams { get; set; }
    }

    public class CombineResponseModel<T> : ResponseModel
    {
        public List<string> ErrorMessages { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int TimeExecute { get; set; }
        public int AmountMockup { get; set; }

        public CombineResponseModel()
        {
            ErrorMessages = new List<string>();
        }
    }

    public class SuccessResponseModel<T> : ResponseModel
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ErrorResponseModel : ResponseModel
    {
        public List<string> ErrorMessages { get; set; }
        public int ErrorCode { get; set; }
    }
}