# Dynamic Mock

#how to use Dynamock
        
        //auto create DI
        var _testSevice = DynaMock.NewInstance<TestService>() 
        
        //get created DI
        _testSevice.GetMock<IRepository5>().Setup(fn => fn.GetNumber()).Returns(x); 
        
        //call public fucntion
        _testSevice.Foo_1(a, b);
        
        //call private function
        _testSevice.InvokeMethod("Foo_2", new object[] { a, b });
        
#Problem 1 : too many DI in our service

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
        
#Solution 1 : create dynamic mock DI
 
        private readonly ITestService _testSevice;
        public TestService_test()
        {
            _testSevice = DynaMock.NewInstance<TestService>();
        }
        
        
#Problem 2 : need to test private function


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
 
        
 #Solution 2 : create Invoke method 
 
        [Fact]
        public void Check_aNotEquelb_ShouldReturn_bPlusx()
        {
            var a = 1;
            var b = 2;
            var x = 3;

            _testSevice.GetMock<IRepository5>().Setup(fn => fn.GetNumber()).Returns(x);
            var result = _testSevice.InvokeMethod("Foo_2", new object[] { a, b });
            Assert.Equal(5, result);
        }
