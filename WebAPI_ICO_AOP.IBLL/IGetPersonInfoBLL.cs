using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.AOP.Attributes;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.IBLL
{
    [AOPInterFaceMark]
    public interface IGetPersonInfoBLL
    {
        [AOPLoginVerify]
        [AOPLimit]
        [AOPLog]
        IBaseModels getInfo();
    }
}
