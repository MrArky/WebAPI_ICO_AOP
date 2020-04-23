using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.ICO
{
    public class MyICOContainerModel
    {
        /// <summary>
        /// 接口类型
        /// </summary>
        public Type type { get; set; }
        /// <summary>
        /// 生命周期类型
        /// </summary>
        public LifeTime lifeTime { get; set; }
        /// <summary>
        /// 用于单例存储
        /// </summary>
        public object SingletonInstance { get; set; }
    }

    /// <summary>
    /// 生命周期枚举
    /// </summary>
    public enum LifeTime
    {
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient,
        /// <summary>
        /// 作用域
        /// </summary>
        Scoped,
        /// <summary>
        /// 单例
        /// </summary>
        Singleton
    }
}
