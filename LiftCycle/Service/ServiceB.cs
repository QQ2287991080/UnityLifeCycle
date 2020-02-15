using LiftCycle.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftCycle.Service
{
    public class ServiceB : IServiceB
    {
        public  ServiceB(IServiceA service)
        {
            Console.WriteLine("B被构造了");
        }
    }
}
