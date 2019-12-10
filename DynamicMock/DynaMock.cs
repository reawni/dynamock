using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DynamicMock
{
    public static class DynaMock
    {

        public static object InvokeMethod<T>(this T obj, string methodName,params object[] param) where T : class
        {
            MethodInfo privMethod = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return privMethod.Invoke(obj, param);
        }
        private static object MockByType(Type type)
        {
            var mock = typeof(Mock<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>());
            return mock.GetType().GetProperties().Single(f => f.Name == "Object" && f.PropertyType == type).GetValue(mock, Array.Empty<object>());
        }
        public static T NewInstance<T>() where T : class
        {
            var constructorInfo = typeof(T).GetConstructors();
            foreach (var item in constructorInfo)
            {
                if (item == null) continue;
                var lobject = item.GetParameters().Select(o => { return MockByType(o.ParameterType); }).ToArray();
                return (T)item.Invoke(lobject);
            }
            return default;
        }
        public static Mock<T> GetMock<T>(this object obj) where T : class
        {
            var f = (T)obj.GetType()
               .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
               .Select(o => o.GetValue(obj))
               .Single(o => o is T);
            return Mock.Get(f);
        }
    }
}
