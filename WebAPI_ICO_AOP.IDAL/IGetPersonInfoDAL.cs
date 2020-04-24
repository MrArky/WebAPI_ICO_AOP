using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.AOP.Attributes;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.IDAL
{
    [AOPInterFaceMark]
    public interface IGetPersonInfoDAL
    {
        IBaseModels getInfo();
    }
}
