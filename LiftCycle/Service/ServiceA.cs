using LiftCycle.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftCycle.Service
{
    public class ServiceA : IServiceA
    {
        public  ServiceA()
        {
            Console.WriteLine("A被构造了");
        }
    }
}
