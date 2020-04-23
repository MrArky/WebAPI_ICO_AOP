using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace WebAPI_ICO_AOP.ICO
{
    /// <summary>
    /// 容器扩展方法
    /// </summary>
    public static class MyICOContainerExtends
    {
        /// <summary>
        /// 创建子容器
        /// </summary>
        /// <returns></returns>
        public static IMyICOContainer CreateChildContainer(this MyICOContainer C)
        {
            //利用反射获取C实例中的MyICOContainerDictionary和MyICOContainerValueDictionary
            Type type = C.GetType();
            var fields = type.GetRuntimeFields();
            const BindingFlags InstanceBindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo MyICOContainerDictionary = type.GetProperty("MyICOContainerDictionary", InstanceBindFlags);
            PropertyInfo MyICOContainerValueDictionary = type.GetProperty("MyICOContainerValueDictionary", InstanceBindFlags);
            return new MyICOContainer((Dictionary<string, MyICOContainerModel>)MyICOContainerDictionary.GetValue(C), (Dictionary<string, object[]>)MyICOContainerValueDictionary.GetValue(C));
        }
    }
}
