using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odin_Toolkits.Common_Utilities
{
    public static class OdinWithReflectionUtil
    {
        // Odin 提供的两个关于反射的工具类
        // Sirenix.Utilities.AssemblyUtilities
        // Sirenix.Utilities.TypeExtensions
        // ---
        public static Assembly[] GetAssembliesOfNameContainString(string partOfAssemblyName)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => assembly.FullName.Contains(partOfAssemblyName))
                    .ToArray();
                return assemblies.Length > 0 ? assemblies : Array.Empty<Assembly>();
            }
            catch (Exception ex)
            {
                // 记录日志或进行其他异常处理
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Array.Empty<Assembly>();
            }
        }

        public static List<string> GetNamespacesInAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var namespaces = types.Select(type => type.Namespace).Where(ns => ns != null).Distinct().ToList();
            return namespaces;
        }
    }
}