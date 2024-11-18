using System.Collections.Generic;
using UnityEngine;

namespace Odin_Toolkits.Common_Utilities
{
    public partial class ProjectUtils
    {
        public static class Random
        {
            /// <summary>
            /// 根据给出的概率运算一次随机
            /// </summary>
            /// <param name="percent"> 0-100，为 true 的概率 </param>
            /// <returns> true or false </returns>
            public static bool GetBool(int percent)
            {
                // 如果 percent = 0 ，右边最小值为 0 ，percent 不可能大于，一定为 false
                // 如果 percent = 100 ，右边最大值为 99 ，percent 不可能小于，一定为 true
                return percent > UnityEngine.Random.Range(0, 100);
            }

            /// <summary>
            /// 随机返回列表中的一个元素
            /// </summary>
            /// <param name="list"> </param>
            /// <typeparam name="T"> </typeparam>
            /// <returns> </returns>
            public static T GetRandomValueFrom<T>(List<T> list)
            {
                return list[UnityEngine.Random.Range(0, list.Count)];
            }

            /// <summary>
            /// 随机返回数组中的一个元素
            /// </summary>
            /// <param name="array"> </param>
            /// <typeparam name="T"> </typeparam>
            /// <returns> </returns>
            public static T GetRandomValueFrom<T>(T[] array)
            {
                return array[UnityEngine.Random.Range(0, array.Length)];
            }

            /// <summary>
            /// 2D 范围随机函数，中心为原点，返回环形中的任意一个点
            /// </summary>
            /// <param name="min"> 最小距离 </param>
            /// <param name="max"> 最大距离 </param>
            /// <returns> 2D 环形中的任意一个点的位置 </returns>
            public static Vector2 RandomDistanceAndPos2D(float min, float max)
            {
                // 首先得出本次随机的长度(两点之间的长度)，比如 7 
                float distance = UnityEngine.Random.Range(min, max);

                // 根据随机确定 X 的符号，Random.Range(0, 2) 只有 0,1 两种可能，整型变量是左闭右开，
                // 如果随机为 1，那么符号为 1，如果随机为 0， 符号为 -1 
                float symbolX = UnityEngine.Random.Range(0, 2) == 1 ? 1f : -1f;

                // 然后随机出一个 X 的值，它的范围是最小距离到本次随机出来的长度
                // 如果 X == distance，就代表 Y 轴为0，如果 X == 0, 那么Y轴为 distance
                float randomX = symbolX * UnityEngine.Random.Range(0, distance);

                // 确定 Y 的符号，如上
                float symbolY = UnityEngine.Random.Range(0, 2) == 1 ? 1f : -1f;

                // 通过开平方得到具体 Y 值，因为 Unity 中 Mathf.Sqrt 只会得出正数，所以要乘以 symbolY
                float posY = symbolY * Mathf.Sqrt(distance * distance - randomX * randomX);

                // 最后整合为一个 Vector2 向量，并返回
                var pos = new Vector2(randomX, posY);
                return pos;
            }

            /// <summary>
            /// 2D 范围随机函数，中心为原点，距离固定，返回圆形的边上的任意一个点
            /// </summary>
            /// <param name="distance"> 距离 </param>
            /// <returns> 返回圆形的边上的任意一个点 </returns>
            public static Vector2 RandomOnlyPos2D(float distance)
            {
                float symbolX = UnityEngine.Random.Range(0, 2) == 1 ? 1f : -1f;
                float randomX = symbolX * UnityEngine.Random.Range(0, distance);

                float symbolY = UnityEngine.Random.Range(0, 2) == 1 ? 1f : -1f;
                float posY = symbolY * Mathf.Sqrt(distance * distance - randomX * randomX);

                var pos = new Vector2(randomX, posY);
                return pos;
            }
        }
    }
}