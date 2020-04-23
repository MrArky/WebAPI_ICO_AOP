using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.ICO.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter| AttributeTargets.Property)]
    public class MyICOParameterRealizeMarkNameAttribute:Attribute
    {
        public string _realizeMarkName { get; private set; }
        public MyICOParameterRealizeMarkNameAttribute(string realizeMarkName)
        {
            this._realizeMarkName = realizeMarkName;
        }
    }
}
