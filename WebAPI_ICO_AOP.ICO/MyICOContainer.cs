using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WebAPI_ICO_AOP.ICO.Attributes;
using System.Reflection;

/**
 * 1.注册
 * 2.对象创建
 * 3.构造函数注入
 * 4.属性注入
 * 5.方法注入
 * 6.单接口多实现
 * 7.注入时允许构造函数参数传入Int、String等值类型参数
 * 8.生命周期管理
 * 
 * 
 * */
namespace WebAPI_ICO_AOP.ICO
{
    /// <summary>
    /// 依赖注入factory类
    /// </summary>
    public class MyICOContainer : IMyICOContainer
    {
        private Dictionary<string, MyICOContainerModel> MyICOContainerDictionary { get; set; } = new Dictionary<string, MyICOContainerModel>();//构造函数注入容器
        private Dictionary<string, object[]> MyICOContainerValueDictionary { get; set; } = new Dictionary<string, object[]>();//常量值类型arguments注入容器
        private Dictionary<string, object> MyICOContainerScopedDictionary { get; set; } = new Dictionary<string, object>();//Scoped作用域单例容器
        public MyICOContainer() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyICOContainer(Dictionary<string, MyICOContainerModel> _MyICOContainerDictionary, Dictionary<string, object[]> _MyICOContainerValueDictionary)
        {
            this.MyICOContainerDictionary = _MyICOContainerDictionary;
            this.MyICOContainerValueDictionary = _MyICOContainerValueDictionary;
        }
        /// <summary>
        /// 通过接口类型的fullName和标记使用哪个接口的实现类的标记名字获取在MyICOContainerDictionary、MyICOContainerValueDictionary中的键值
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="realizeMarkName"></param>
        /// <returns></returns>
        private string getKey(string fullName, string realizeMarkName) => $"{fullName}♣▣✪✣♪{realizeMarkName}";//♣▣✪✣♪特殊符号不容易被后期使用者命名时命中
        /// <summary>
        /// 注册方法
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="realizeMarkName"></param>
        /// <param name="arguments"></param>
        /// <param name="lifeTime"></param>
        public void Register<TFrom, TTo>(string realizeMarkName = null, object[] arguments = null, LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom//泛型约束，即TTo必须是TFrom的实现，也可理解为TTo必须继承TFrom
        {
            MyICOContainerDictionary.Add(this.getKey(typeof(TFrom).FullName, realizeMarkName), new MyICOContainerModel() { type = typeof(TTo), lifeTime = lifeTime });
            MyICOContainerValueDictionary.Add(this.getKey(typeof(TFrom).FullName, realizeMarkName), arguments);
        }
        /// <summary>
        /// 根据接口创建对象
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="realizeMarkName"></param>
        /// <returns></returns>
        public TFrom Resolve<TFrom>(string realizeMarkName = null)
        {
            return (TFrom)this.CreateResolveObjectByType(typeof(TFrom), realizeMarkName);
        }
        /// <summary>
        /// Resolve方法递归创建对象辅助方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="realizeMarkName"></param>
        /// <returns></returns>
        private object CreateResolveObjectByType(Type type, string realizeMarkName = null)
        {
            string key = this.getKey(type.FullName, realizeMarkName);
            type = MyICOContainerDictionary[key].type;
            #region 生命周期管理
            switch (this.MyICOContainerDictionary[key].lifeTime)
            {
                case LifeTime.Transient:
                    //Do Nothing
                    break;
                case LifeTime.Scoped:
                    if (this.MyICOContainerScopedDictionary.ContainsKey(key)) return this.MyICOContainerScopedDictionary[key];
                    break;
                case LifeTime.Singleton:
                    if (this.MyICOContainerDictionary[key].SingletonInstance != null) return this.MyICOContainerDictionary[key].SingletonInstance;
                    break;
                default:
                    break;
            }
            #endregion
            #region 构造函数注入
            //如果有声明MyICOConstuctorInjectionAttribute特性，使用申明的参数最长的那个,否则直接使用构造函数最长的那个（特性优先、参数数量多者优先原则）
            ConstructorInfo constuctor = type.GetConstructors().Where(item => item.IsDefined(typeof(MyICOConstuctorInjectionAttribute), true)).Count() > 0
                                       ? type.GetConstructors().Where(item => item.IsDefined(typeof(MyICOConstuctorInjectionAttribute), true)).OrderByDescending(item => item.GetParameters().Length).First()
                                       : type.GetConstructors().OrderByDescending(item => item.GetParameters().Length).First();
            //将所有的参数保存起来
            List<object> listPara = new List<object>();
            //找出所有的常量值类型参数
            object[] objValues = MyICOContainerValueDictionary[key];
            int constIndex = 0;
            //遍历构造函数中所有的参数
            foreach (ParameterInfo para in constuctor.GetParameters())
            {
                if (para.IsDefined(typeof(MyICOParameterConstAttribute), true))
                    listPara.Add(objValues[constIndex++]);
                else
                    //参数可能又是另一个对象的依赖，比如Controller依赖BLL，BLL又依赖DAL，DAL继续依赖了Models
                    listPara.Add(this.CreateResolveObjectByType(para.ParameterType, this.realizeMarkName(para)));//递归
            }
            #endregion
            var objInstance = Activator.CreateInstance(type, listPara.ToArray());
            #region 属性注入
            var objInstancePropertys = type.GetProperties().Where(item => item.IsDefined(typeof(MyICOPropertyInjectionAttribute), true));//获取所有需要做属性注入的属性
            foreach (PropertyInfo prop in objInstancePropertys)
            {
                prop.SetValue(objInstance, this.CreateResolveObjectByType(prop.PropertyType, this.realizeMarkName(prop)));
            }
            #endregion
            #region 方法注入(此注入使用非常少)
            var objInstanceMethods = type.GetMethods().Where(item => item.IsDefined(typeof(MyICOMethodInjectionAttribute), true));//获取所有需要做方法注入的属性
            foreach (MethodInfo method in objInstanceMethods)
            {
                listPara = new List<object>();
                foreach (ParameterInfo para in method.GetParameters())
                {
                    listPara.Add(this.CreateResolveObjectByType(para.ParameterType, this.realizeMarkName(para)));//递归
                }
                method.Invoke(objInstance, listPara.ToArray());
            }
            #endregion
            #region 生命周期管理
            switch (MyICOContainerDictionary[key].lifeTime)
            {
                case LifeTime.Transient:
                    //Do Nothing
                    break;
                case LifeTime.Scoped:
                    this.MyICOContainerScopedDictionary[key] = objInstance;
                    break;
                case LifeTime.Singleton:
                    this.MyICOContainerDictionary[key].SingletonInstance = objInstance;
                    break;
                default:
                    break;
            }
            #endregion
            return objInstance;
        }

        /// <summary>
        /// 获取依赖接口的标记（主要识别单接口多实现）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string realizeMarkName(ICustomAttributeProvider p)
        {
            return p.IsDefined(typeof(MyICOParameterRealizeMarkNameAttribute), true) ? ((MyICOParameterRealizeMarkNameAttribute)(p.GetCustomAttributes(typeof(MyICOParameterRealizeMarkNameAttribute), true)[0]))._realizeMarkName : null;
        }
    }
}
