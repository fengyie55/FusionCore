# FusionUI 模块约定

## 目的

本文件描述 `FusionUI` 当前阶段的职责边界、最小 Shell 骨架和导航组织方式。

当前阶段目标：
- 提供最小 WPF Shell
- 提供最小导航与视图区组织
- 为后续 Host / Config / Log / Scheduler / FA 接线保留清晰入口

---

## 当前新增能力

`FusionUI` 第二阶段之前的基础阶段，当前已经具备：
- 最小 Shell 窗口
- 顶部状态区、左侧导航区、中央工作区、底部消息区
- 概览、操作员、工程师、运行、日志、设备 六个占位页面
- 最小导航模型与路由表达
- 最小 UI 组合入口

---

## 职责边界

`FusionUI` 负责：
- 表现层壳
- 页面导航
- 工作区承载
- 最小页面占位

`FusionUI` 不负责：
- 业务真相
- 调度算法
- 设备控制
- FA 协议逻辑
- 配置系统实现
- 日志系统实现
- 进程管理或宿主实现

---

## Shell 与导航

当前默认骨架由以下部分组成：
- `Shell/MainWindow`
- `Shell/ShellViewModel`
- `Navigation/NavigationItem`
- `Navigation/NavigationSection`
- `Navigation/NavigationViewModel`
- `Composition/UiCompositionRoot`

当前导航只提供最小静态入口：
- 概览
- 操作员
- 工程师
- 运行
- 日志
- 设备

当前不实现：
- 权限驱动菜单
- 动态插件菜单
- 复杂路由系统
- 页面参数系统

---

## 占位视图

当前视图只作为未来页面入口占位：
- `OverviewView`
- `OperatorView`
- `EngineerView`
- `RuntimeView`
- `LogsView`
- `EquipmentView`

这些页面当前只显示标题、说明和提示信息，不承载真实业务功能。

---

## 最小 UI 接线边界

`UiCompositionRoot` 与 `UiRuntimeDescriptor` 用于表达：
- UI 壳如何被构造
- 当前导航分区有哪些
- 当前布局语义是什么

它们当前解决的是“最小可接线”的问题，而不是完整 UI 启动框架。

当前不解决：
- Host 启动集成
- Config 注入实现
- Log 面板实现
- Scheduler / FA 数据接入
- 多进程消息桥接

---

## 多进程约束

FusionUI 必须保持多进程友好：
- 不假设 UI 直接持有后台模块内部对象
- 不把页面状态当作业务真相
- 不把 DomainEvent 直接当成 UI 事件总线
- 后续接入 Host / Config / Log / Scheduler / FA 时，应通过显式快照、查询或适配边界完成

---

## 后续接入方向

后续建议按以下方向继续：
1. 接入最小 UI 配置边界
2. 接入最小日志展示入口占位
3. 接入最小运行状态摘要模型
4. 再逐步接入 Scheduler / FA 的只读投影视图
