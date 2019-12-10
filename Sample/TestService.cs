using System;

namespace Sample
{

    public interface ITestService
    {
        int Foo_1(int a, int b);
    }

    public class TestService : ITestService
    {
        private readonly IRepository1 repository1;
        private readonly IRepository2 repository2;
        private readonly IRepository3 repository3;
        private readonly IRepository4 repository4;
        private readonly IRepository5 repository5;

        public TestService(IRepository1 repository1,
            IRepository2 repository2,
            IRepository3 repository3,
            IRepository4 repository4,
            IRepository5 repository5
            )
        {
            this.repository1 = repository1;
            this.repository2 = repository2;
            this.repository3 = repository3;
            this.repository4 = repository4;
            this.repository5 = repository5;
        }

        public int Foo_1(int a, int b)
        {
            return Foo_2(a, b);
        }

        private int Foo_2(int a, int b)
        {
            if (a == b)
            {
                var x = repository1.GetNumber();
                return x + a;
            }
            else
            {
                var x = repository5.GetNumber();
                return x + b;
            }
        }
    }
}
