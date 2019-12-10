using Sample;
using System;
using Xunit;

namespace DynamicMock
{
    public class TestService_test
    {
        private readonly ITestService _testSevice;
        public TestService_test()
        {
            _testSevice = DynaMock.NewInstance<TestService>();
        }

        [Fact]
        public void Check_aEquelb_ShouldReturn_aPlusx()
        {
            var a = 2;
            var b = 2;
            var x = 2;

            _testSevice.GetMock<IRepository1>().Setup(fn => fn.GetNumber()).Returns(x);
            var result = _testSevice.Foo_1(a, b);

            Assert.Equal(4, result);
        }

        [Fact]
        public void Check_aNotEquelb_ShouldReturn_bPlusx()
        {
            var a = 1;
            var b = 2;
            var x = 3;

            _testSevice.GetMock<IRepository5>().Setup(fn => fn.GetNumber()).Returns(x);
            var result = _testSevice.InvokeMethod("Foo_2", a, b);
            Assert.Equal(5, result);
        }
    }
}
