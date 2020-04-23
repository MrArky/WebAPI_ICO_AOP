using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.IDAL;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.DAL
{
    public class GetPersonInfoDAL : IGetPersonInfoDAL
    {
        private readonly IBaseModels _baseModel =null;
        public GetPersonInfoDAL(IBaseModels baseModel)//构造函数注入
        {
            this._baseModel = baseModel;
        }
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public IBaseModels getInfo()
        {
            return _baseModel;
        }
    }
}
