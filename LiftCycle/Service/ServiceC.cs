using LiftCycle.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftCycle.Service
{
    public class ServiceC : IServiceC
    {
        public  ServiceC(IServiceB service)
        {
            Console.WriteLine("C被构造了");
        }
    }
}
