using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_ICO_AOP.IModels;

namespace WebAPI_ICO_AOP.Models
{
    public class Person : IBaseModels
    {
        public string Name { get; set; } = "Mr.Arky";
        public int Age { get; set; } = 28;

        public decimal Weight { get; set; } = 73.9M;
    }
}
