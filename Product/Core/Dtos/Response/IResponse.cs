using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Response
{
    public interface IResponse
    {
        bool IsSucceed { get; set; }
    }
}