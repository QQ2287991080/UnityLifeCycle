using LiftCycle.IService;
using LiftCycle.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.Resolution;

namespace LiftCycle
{
    class Program
    {
        static void Main(string[] args)
        {
            ///IOC容器 的作用帮程序能够初始化对象，那我们要做的就是把需要初始化的对象放到容器中，其他的就需要我们去管了，用的时候取就行。
            ///众所周知IOC容器里面非常强调一个生命周期的东西，这个例子会用来剖析IOC容器的生命周期是怎么一回事。


            ///首先来看看我们平常所做的实例化一个对象是怎么做的
            ///现在有Service ABC，ServiceB用一个有参数ServiceA的构造函数，而ServiceC有一个含参数的ServiceB的构造函数
            ///所以来实现一下吧

            {

                ServiceA serviceA = new ServiceA();
                ServiceB serviceB = new ServiceB(serviceA);
                ServiceC serviceC = new ServiceC(serviceB);
                ///由上面的代码来实例化类的话其实是非常类的一件事，我这里只有三个类，如果是几十个，上百个的话你想想是多么累的一件事。
            }

            IUnityContainer container = new UnityContainer();
            //实例
            container.RegisterSingleton<IServiceA, ServiceA>();//单例
            container.RegisterType<IServiceB, ServiceB>();//瞬时

            {
                var a1 = container.Resolve<IServiceA>();
                var a2 = container.Resolve<IServiceA>();
                Console.WriteLine(object.ReferenceEquals(a1, a2));//运行之后获得结果是true，那说明这个对象是同一个，这就说明了单例的效果，容器初始化，程序全局只有一个单例。
            }
            {
                var b1 = container.Resolve<IServiceB>();
                var b2 = container.Resolve<IServiceB>();
                Console.WriteLine(object.ReferenceEquals(b1, b2));//运行之后得到false，说明两个对象是不相等的，那么其实和我们平时new一个对象是一样的。
            }
            {
                //创建子容器
                var child = container.CreateChildContainer();
                var child2 = container.CreateChildContainer();
                var b1 = child.Resolve<IServiceB>();
                var b2 = child2.Resolve<IServiceB>();
                Console.WriteLine(object.ReferenceEquals(b1, b2));
                //这个地方说明一件事，那就是就算父容器注册了IServiceB，但是子容器都能解析的类型都是属于自己的实例，容器将返回父容器创建的实例。
            }
            //所以再有了容器之后我们就不需要自己初始化容器，容器帮我把把活做了，再回到之前的例子，我现在需要实例化一个ServiceC那怎么去做呢，
            //首先肯定把IServiceC放入容器在需要是要的地方，使用构造函数注入就行使用了，而且在控制台打印的东西也是能够发现端倪的，我们并没有实例化ServiceC那为什么控制台会提示“ServiceC被构造了”呢，那就是因为容器帮忙做了。
            //为什么会这么强调生命周期呢，因为我们如果利用好生命周期的话是能够提升性能的，比如我们有一个不经常用的Service（不一定是Service结合自己的业务需求）我不想每次都去实例化，想全局只要一个，那么这个时候使用RegisterSingleton注册，不就很好的满足了我们的需求了，譬如一些数据库连接就是使用的单例模式，那么我们就可以将它放到容器中，让容器帮我们管理。
            Console.ReadKey();
        }
    }
}
