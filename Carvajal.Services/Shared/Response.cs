using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carvajal.Services.Shared
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

    }
}
