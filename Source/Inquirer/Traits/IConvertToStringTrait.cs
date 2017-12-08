using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IConvertToStringTrait<TResult>
    {
        IConvertToStringComponent<TResult> Convert { get; set; }
    }
}
