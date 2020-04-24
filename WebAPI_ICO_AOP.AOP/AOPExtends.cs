using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.AOP
{
    public static class AOPExtends
    {
        public static object AOP(this object o,Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            IOCInterceptor interceptor = new IOCInterceptor();
            return generator.CreateInterfaceProxyWithTarget(interfaceType, o, interceptor);
        }
    }
}
