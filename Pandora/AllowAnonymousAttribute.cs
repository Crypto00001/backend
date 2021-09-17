using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
