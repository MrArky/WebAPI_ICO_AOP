using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.IBLL;
using WebAPI_ICO_AOP.ICO.Attributes;
using WebAPI_ICO_AOP.IDAL;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.BLL
{
    public class GetPersonInfoTwoBLL : IGetPersonInfoBLL
    {
        /// <summary>
        /// 属性注入
        /// </summary>
        [MyICOPropertyInjection]
        [MyICOParameterRealizeMarkName("Two")]
        public IGetPersonInfoDAL _getPersonInfoDAL1 { get; set; }
        public IBaseModels getInfo()
        {
            return _getPersonInfoDAL1.getInfo();
        }
    }
}
