using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.AOP.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class AOPInterFaceMarkAttribute : Attribute
    {
    }
}
