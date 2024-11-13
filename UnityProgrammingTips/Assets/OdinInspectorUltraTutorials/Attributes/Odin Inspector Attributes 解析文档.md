# Odin Inspector Attributes 解析文档

- Odin Inspector 起始版本 3.3.1.10

## Essentials

## Debug

### ShowDrawerChain

- 显示属性的绘制过程，便于调试
- 灰色显示的 Drawer 流程表示未被使用的 Drawer ，通常是有同级别的 Drawer 覆盖了其他 Drawer ，而不是简单的跳过

---

#### 使用 Odin 的基础绘制流程

一定有三个 Drawer，且分三个优先级：（实际上还有其他等级的优先级）

1. SuperPriority 
   1. Mostly used by drawers that wants to wrap the entire property but don't draw the actual property. These drawers typically don't draw the property itself, and calls `CallNextDrawer()`.
   2. 主要用于想要包裹整个属性但不绘制实际属性的抽屉。这些抽屉通常不绘制属性本身，并调用`CallNextDrawer()`。
2. WrapperPriority
   1. Mostly used by drawers used to decorate properties.
   2. 主要用于装饰房屋的抽屉。
3. ValuePriority
   1. Mostly used by `OdinValueDrawer<T>` and `OdinAttributeDrawer<TAttribute,TValue>`.
   2. 主要由`OdinValueDrawer<T>` 和 `OdinAttributeDrawer<TAttribute,TValue>`使用。

##### 部分场景截图

<img src="./Odin Inspector Attributes 解析文档.assets/int基础类型.png" style="zoom:80%;" />

<img src="./Odin Inspector Attributes 解析文档.assets/string基础类型.png" alt="string基础类型" style="zoom:80%;" />

<img src="./Odin Inspector Attributes 解析文档.assets/float基础类型.png" style="zoom:80%;" />

##### 截图分析

- 最后一个 `CompositeDrawer` 是默认被覆盖的，灰色表示未使用，只有把其他 `ValuePriority` 优先级的关闭，才会使用，作用应该为 Unity 内置的原始的绘制方式
  - 猜测理由：在官方案例 `CutomDrawers` 中的 `Priority Examples` 中，如果在自定义的 `ValuePriority` 中，只选择绘制一个字段，开启自定义的`ValuePriority` 时，只显示一个字段，而跳过这个自定义的 `ValuePriority`，显示出`CompositeDrawer`  时，反而有两个字段绘制，如果全部跳过，则不显示任何字段，因此它可以理解为 Unity 内置的原始的绘制方式
- `float`类型对应的Drawer叫`SingleDrawer`，与其他不同
- ValuePriority 级别只能有一个 Drawer 正在绘制，多余的会进行覆盖，Unity 原生的优先级最低，其他两个级别可以同时使用（绘制）

1. 值类型有3个要点
   1. 属性的右键绘制，如果关闭，则无法打开属性的右键菜单
   2. int 的验证性绘制，此时关闭效果不明显，可能在其他类型上有效果
   3. 关于这个类型直接的 Drawer
2. 引用类型有5个要点
   1. 属性的右键绘制
   2. 引用类型的绘制
   3. 根据名称可以理解为对 Unity 内置绘制的修复，不探究
   4. 常规的验证性绘制
   5. 关于引用类型的直接对应的 Drawer
