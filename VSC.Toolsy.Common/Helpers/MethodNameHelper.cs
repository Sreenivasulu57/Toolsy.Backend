using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VSC.Toolsy.Common.Helpers
{
    public static class MethodNameHelper
    {
        public static string GetMethodContextName()
        {
            try
            {
                return new StackTrace().GetFrame(1).GetMethod().GetMethodContextName();
            }
            catch { }
            return "UNKNOWN Method";
        }

        private static string GetMethodContextName(this MethodBase method)
        {
            if (method.DeclaringType.GetInterfaces().Any(i => i == typeof(IAsyncStateMachine)))
            {
                var generatedType = method.DeclaringType;
                var originalType = generatedType.DeclaringType;
                var foundMethod = originalType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)
                    .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
                return foundMethod.DeclaringType.Name + "." + foundMethod.Name;
            }
            else
            {
                return method.DeclaringType.Name + "." + method.Name;
            }
        }
    }
}
