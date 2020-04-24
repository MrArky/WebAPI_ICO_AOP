using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.AOP.Attributes;

namespace WebAPI_ICO_AOP.AOP
{
    class IOCInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation) { }
        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Action action = () => base.PerformProceed(invocation);//此处才是真实调用方法，因此依然可以在他之前和之后做一定的事情
            var method = invocation.Method;
            foreach (var attr in method.GetCustomAttributes(typeof(AOPBaseAttribute), true))
            {
                var type = attr.GetType();
                action = (Action)type.GetMethod("Do").Invoke(attr, new object[] { invocation, action });
            }
            action.Invoke();
        }
        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation) { }
    }
}
