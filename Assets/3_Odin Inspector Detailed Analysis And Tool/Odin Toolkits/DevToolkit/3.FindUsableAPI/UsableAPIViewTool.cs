using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Odin_Toolkits.DevToolkit._3.FindUsableAPI
{
    public static class UsableAPIViewTool
    {
        static readonly Dictionary<string, string[]> MethodInfoCache = new();

        /// <summary>
        /// 筛选出指定类型的所有 get、set 和事件方法
        /// </summary>
        public static string[] FilterGetAndSetOrEventMethods(Type selectedType)
        {
            var methods = GetPublicAndProtectedMethodsWithParameters(selectedType);
            return methods.Where(m =>
                m.Contains("get_") || m.Contains("set_") || m.Contains("add_") || m.Contains("remove_")).ToArray();
        }

        /// <summary>
        /// 筛选出指定类型的所有公共和受保护非静态方法，不包括 get、set 和事件方法
        /// </summary>
        public static string[] GetPublicAndProtectedMethods(Type selectedType)
        {
            var methods = GetPublicAndProtectedMethodsWithParameters(selectedType);
            return methods.Where(m => !m.Contains("get_") && !m.Contains("set_") && !m.Contains("add_") &&
                                      !m.Contains("remove_") && !m.Contains("static")).ToArray();
        }

        /// <summary>
        /// 筛选出指定类型的公共和受保护静态方法，不包括 get、set 和事件方法
        /// </summary>
        public static string[] GetPublicAndProtectedStaticMethods(Type selectedType)
        {
            var methods = GetPublicAndProtectedMethodsWithParameters(selectedType);
            return methods.Where(m => !m.Contains("get_") && !m.Contains("set_") && !m.Contains("add_") &&
                                      !m.Contains("remove_") && m.Contains("static")).ToArray();
        }

        /// <summary>
        /// 获取指定类型的所有公共和受保护方法
        /// </summary>
        public static IEnumerable<string> GetPublicAndProtectedMethodsWithParameters<T>()
        {
            var type = typeof(T);
            return GetPublicAndProtectedMethodsWithParameters(type);
        }

        public static IEnumerable<string> GetPublicAndProtectedMethodsWithParameters(Type selectedType)
        {
            // 尝试从缓存中获取方法信息
            if (selectedType.FullName != null &&
                MethodInfoCache.TryGetValue(selectedType.FullName, out string[] methodInfos))
                return methodInfos;

            try
            {
                var instanceMethods = selectedType.GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                              BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                var staticMethods = selectedType.GetMethods(BindingFlags.Static | BindingFlags.Public |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.DeclaredOnly);
                var result = new List<string>(instanceMethods.Length + staticMethods.Length);

                foreach (var method in instanceMethods)
                    if (method.IsPublic || method.IsFamily)
                    {
                        string parameters = string.Join(", ",
                            method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        result.Add(ReplaceToNormal(
                            $"{(method.IsPublic ? "public" : "protected")} {method.ReturnType.Name} {method.Name}({parameters})"));
                    }

                result.AddRange(from method in staticMethods
                    where method.IsPublic || method.IsFamily
                    let parameters =
                        string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))
                    select
                        ReplaceToNormal(
                            $"{(method.IsPublic ? "public" : "protected")} static {method.ReturnType.Name} {method.Name}({parameters})"));

                string[] resultArr = result.ToArray();
                // 将方法信息缓存起来
                if (selectedType.FullName != null) MethodInfoCache[selectedType.FullName] = resultArr;
                return resultArr;
            }
            catch (Exception ex)
            {
                // 反射操作失败的异常处理逻辑
                return new[] { ex.Message };
            }
        }

        static string ReplaceToNormal(string str)
        {
            return str.Replace("Single", "float").Replace("Int32", "int").Replace("Boolean", "bool").Replace("Void",
                    "void").Replace("String", "string").Replace("Single[]", "float[]")
                .Replace("Int32[]", "int[]").Replace("Boolean[]", "bool[]").Replace("String[]", "string[]")
                .Replace("Object[]", "object[]").Replace(" Object ", " object ");
        }
    }
}