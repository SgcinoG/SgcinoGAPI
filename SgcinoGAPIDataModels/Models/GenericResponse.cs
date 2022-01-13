using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgcinoGAPIDataModels.Models
{
    public class GenericResponse
    {
        public ResponseStatus Status { get; set; }
        public Error Error { get; set; }
    }

    public enum ResponseStatus
    {
        Pending,
        Success,
        Fail,
        Timeout,
        Error,
        Blocked
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
