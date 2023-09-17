using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Utilities
{
    public static class LinqEnumToStringHelper
    {
        public static string ConvertString(dynamic data)
        {
           return string.Join(string.Empty, data.Reverse());
        }
    }
}
