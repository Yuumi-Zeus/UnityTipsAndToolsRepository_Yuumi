using System;
using UnityEngine;

namespace Odin_Toolkits.Inspector.CustomInspectorTutorial._3.自定义数据结构类
{
    [Serializable]
    public class 自定义数据结构
    {
        public int intValue;
        public string stringValue;
    }

    public class 自定义数据结构类 : MonoBehaviour
    {
        public 自定义数据结构 customData = new();

        // Start is called before the first frame update
        void Start() { }

        // Update is called once per frame
        void Update() { }
    }
}