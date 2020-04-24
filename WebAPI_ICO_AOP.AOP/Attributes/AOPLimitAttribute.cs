using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.AOP.Attributes
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPLimitAttribute : AOPBaseAttribute
    {
        public Action Do(object Instance, Action action)
        {
            return () =>
            {
                Console.WriteLine("AOPLimitAttribute:Start");
                action.Invoke();
                Console.WriteLine("AOPLimitAttribute:End");
            };
        }
    }
}
