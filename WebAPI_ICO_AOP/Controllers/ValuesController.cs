using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ICO_AOP.BLL;
using WebAPI_ICO_AOP.DAL;
using WebAPI_ICO_AOP.IBLL;
using WebAPI_ICO_AOP.ICO;
using WebAPI_ICO_AOP.IDAL;
using WebAPI_ICO_AOP.IModels;
using WebAPI_ICO_AOP.Models;
using static WebAPI_ICO_AOP.ICO.MyICOContainer;

namespace WebAPI_ICO_AOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IGetPersonInfoBLL _getPersonInfoBLL = null;
        private readonly IGetPersonInfoBLL _getPersonInfoBLL1 = null;

        private readonly IGetPersonInfoBLL _getPersonInfoBLLScoped1 = null;
        private readonly IGetPersonInfoBLL _getPersonInfoBLLScoped2 = null;
        public ValuesController()//构造函数注入
        {
            #region 在此使用自己ICO框架进行依赖注入
            var myICO = new MyICOContainer();
            myICO.Register<IGetPersonInfoDAL, GetPersonInfoDAL>();
            myICO.Register<IGetPersonInfoDAL, GetPersonInfoTwoDAL>("Two");//同时传入arguments参数
            myICO.Register<IGetPersonInfoBLL, GetPersonInfoBLL>(arguments: new object[] { "abc", 12 }, lifeTime: LifeTime.Singleton);
            myICO.Register<IGetPersonInfoBLL, GetPersonInfoTwoBLL>("Two", lifeTime: LifeTime.Transient);
            myICO.Register<IBaseModels, Person>();
            #endregion
            this._getPersonInfoBLL = myICO.Resolve<IGetPersonInfoBLL>();
            this._getPersonInfoBLL1 = myICO.Resolve<IGetPersonInfoBLL>();
            var context=myICO.CreateChildContainer();//此处实现了作用域单例
            _getPersonInfoBLLScoped1 = ((MyICOContainer)context).Resolve<IGetPersonInfoBLL>("Two");
            _getPersonInfoBLLScoped2 = ((MyICOContainer)context).Resolve<IGetPersonInfoBLL>("Two");
        }
        // GET api/values
        [HttpGet]
        public ActionResult<Person> Get()
        {
            Console.WriteLine(_getPersonInfoBLLScoped1 == _getPersonInfoBLLScoped2);
            Console.WriteLine(_getPersonInfoBLL == _getPersonInfoBLL1);
            Person _m = (Person)_getPersonInfoBLL.getInfo();
            return _m;
        }
    }
}
