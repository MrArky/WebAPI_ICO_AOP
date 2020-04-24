using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.AOP.Attributes;
using WebAPI_ICO_AOP.IBLL;
using WebAPI_ICO_AOP.ICO.Attributes;
using WebAPI_ICO_AOP.IDAL;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.BLL
{
    public class GetPersonInfoBLL : IGetPersonInfoBLL
    {
        private readonly IBaseModels _baseModel = null;
        private readonly IGetPersonInfoDAL _getPersonInfoDAL = null;
        private IGetPersonInfoDAL _getPersonInfoDAL3 = null;//注意方法注入的接口对象引用不能设置为只读
        private IGetPersonInfoDAL _getPersonInfoDAL4 = null;
        /// <summary>
        /// 属性注入
        /// </summary>
        [MyICOPropertyInjection]
        [MyICOParameterRealizeMarkName("Two")]
        public IGetPersonInfoDAL _getPersonInfoDAL1 { get; set; }
        /// <summary>
        /// 没有加特性标注的构造函数
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="getPersonInfoDAL"></param>
        public GetPersonInfoBLL(IBaseModels baseModel, IGetPersonInfoDAL getPersonInfoDAL)//构造函数注入
        {
            this._baseModel = baseModel;
            this._getPersonInfoDAL = getPersonInfoDAL;
        }
        /// <summary>
        /// 加了特性标注的构造函数(即使参数不是最多的一个构造函数，但是特性优先、参数数量多者优先原则)
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="getPersonInfoDAL"></param>
        [MyICOConstuctorInjection]
        public GetPersonInfoBLL([MyICOParameterConst]string s,IGetPersonInfoDAL getPersonInfoDAL, [MyICOParameterConst]int i)//构造函数注入
        {
            this._getPersonInfoDAL = getPersonInfoDAL;
        }
        /// <summary>
        /// 方法注入
        /// </summary>
        /// <param name="getPersonInfoDAL"></param>
        [MyICOMethodInjection]
        public void FuncInjection(IGetPersonInfoDAL getPersonInfoDAL)
        {
            this._getPersonInfoDAL3 = getPersonInfoDAL;
        }
        /// <summary>
        /// 方法注入(不带特性是无法注入的)
        /// </summary>
        /// <param name="getPersonInfoDAL"></param>
        public void FuncInjection2(IGetPersonInfoDAL getPersonInfoDAL)
        {
            this._getPersonInfoDAL4 = getPersonInfoDAL;
        }
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public IBaseModels getInfo()
        {
            Console.WriteLine("————————这是方法自身执行的内容");
            return _getPersonInfoDAL.getInfo();
        }
    }
}
