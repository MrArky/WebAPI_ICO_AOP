using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.IDAL;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.DAL
{
    public class GetPersonInfoTwoDAL : IGetPersonInfoDAL
    {
        private readonly IBaseModels _baseModel = null;
        public GetPersonInfoTwoDAL(IBaseModels baseModel)//构造函数注入
        {
            this._baseModel = baseModel;
        }
        public IBaseModels getInfo()
        {
            return _baseModel;
        }
    }
}
