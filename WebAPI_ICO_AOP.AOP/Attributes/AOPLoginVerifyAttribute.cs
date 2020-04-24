using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.AOP.Attributes
{
    /// <summary>
    /// 登陆过期验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AOPLoginVerifyAttribute: AOPBaseAttribute
    {
        public Action Do(object Instance, Action action)
        {
            return () =>
            {
                Console.WriteLine("AOPLoginVerifyAttribute:Start");
                action.Invoke();
                Console.WriteLine("AOPLoginVerifyAttribute:End");
            };
        }
    }
}
