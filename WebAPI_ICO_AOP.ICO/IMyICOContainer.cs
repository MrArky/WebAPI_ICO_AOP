using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_ICO_AOP.ICO
{
    public interface IMyICOContainer
    {
        void Register<TFrom, TTo>(string realizeMarkName = null, object[] arguments = null, LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom;
        TFrom Resolve<TFrom>(string realizeMarkName = null);
    }
}
