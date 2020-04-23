using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.ICO.Attributes
{
    /// <summary>
    /// 构造函数注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]//申明只用于构造函数使用
    public class MyICOConstuctorInjectionAttribute:Attribute
    {
    }
}
