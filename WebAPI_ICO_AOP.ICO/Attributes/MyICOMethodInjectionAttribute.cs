using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.ICO.Attributes
{
    /// <summary>
    /// 方法注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MyICOMethodInjectionAttribute:Attribute
    {
    }
}
