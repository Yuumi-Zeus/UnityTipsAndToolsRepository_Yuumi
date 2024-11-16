# How To Serialize Interface

GitHub链接：[Unity Tips And Tools Repository](https://github.com/Yuumi-Zeus/UnityTipsAndToolsRepository_Yuumi)

原版使用技巧视频链接：[How to Serialize Interfaces in Unity (Drag and Drop Support) - Youtube](https://www.youtube.com/watch?v=xcGPr04Mgm4&t=158s)

原版 Github 链接：[adammyhre/Unity-Serialized-Interface](https://github.com/adammyhre/Unity-Serialized-Interface)

原版不依赖 Odin 插件实现，可以和 Odin 共存

视频名称为序列化接口，但并没有像 Odin Serializer 那样真正的序列化了接口，实际底层是使用了 TObject 泛型类型去存储任何实现了 TInterface 泛型接口的实例对象。然后封装绘制，没有实现对应接口的对象无法存储

笔者认为叫做“接口引用选择器”更合适

---

此版本的 OdinInterfaceReference 依赖 Odin 插件，使用了 Odin 绘制

简单版本是将 TObject 设定为 Object，这样即可存储 Unity 中所有对象，因为都继承自 Object

实现解析可以查看对应解析文档

---

实现了一个自定义类 `OdinInterfaceReference<TInterface,TObject> where TInterface : class where TObject : UnityEngine.Object`

可以快捷地使用接口类型的对象，也可以直接获取原类型的对象

---

Unity 原生版本的请查看原版 Github 链接，已在本项目中移除