using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.AOP.Attributes
{
    /// <summary>
    /// 记录日志
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPLogAttribute: AOPBaseAttribute
    {
        public Action Do(object Instance, Action action)
        {
            return () =>
            {
                Console.WriteLine("AOPLogAttribute:Start");
                action.Invoke();
                Console.WriteLine("AOPLogAttribute:End");
            };
        }
    }
}
