# FusionUI 模块约定

## 当前阶段

`FusionUI` 当前处于从 `P2` 向“更稳定最小接线骨架”推进的阶段。

本轮新增的重点能力：
- `UiSection -> UiShellOptions` 的最小映射边界
- 运行态摘要只读模型
- 日志入口摘要只读模型
- 状态栏模型
- `UiBootstrapContext` 与 `UiCompositionRoot` 的最小接线入口

---

## 模块职责

`FusionUI` 负责：
- WPF Shell
- 导航骨架
- 视图区组织
- 只读摘要展示模型
- 最小 UI 接线入口

`FusionUI` 不负责：
- 业务真相
- 调度逻辑
- 设备控制
- FA 协议逻辑
- 配置系统实现
- 日志系统实现
- 完整宿主运行时

---

## 当前默认接线方式

当前最小接线链路如下：

1. `FusionConfig.UiSection`
2. `FusionUI.Projections.UiOptionsBinder`
3. `UiSectionMappingResult`
4. `UiBootstrapContext`
5. `UiCompositionRoot.CreateShell(...)`

运行态与日志摘要也通过 `UiBootstrapContext` 显式传入：
- `RuntimeSummaryModel`
- `LogsViewProjection`

这种方式解决的问题是：
- UI 可以消费外部模块提供的只读摘要
- UI 不必直接持有后台内部对象
- Host / App 后续可以显式装配 UI，而不是依赖全局静态状态

---

## 运行态摘要与日志摘要

### 运行态摘要

当前运行态摘要模型包括：
- `RuntimeSummaryModel`
- `HostRuntimeSummaryModel`
- `ModuleRuntimeSummaryModel`

它们只表达最小只读信息：
- Host 名称
- Host 状态
- 初始化状态
- Runtime Instance Id
- Profile
- Runtime Root
- 模块状态摘要

### 日志摘要

当前日志入口摘要模型包括：
- `LogEntrySummaryModel`
- `LogsViewProjection`

它们只表达最小只读信息：
- 时间
- 级别
- 分类
- 消息摘要
- 来源摘要

当前不实现日志浏览、过滤、搜索、实时推送或日志平台。

---

## 状态栏模型

当前状态栏由以下模型承载：
- `StatusBarModel`
- `StatusBarItem`
- `UiStatusMessage`

当前只表达最小摘要：
- 当前页面
- Host 状态
- Profile
- Runtime Root
- 最小消息文本

状态栏不是业务控制面板，也不是运行时控制台。

---

## 它解决什么问题

当前阶段解决的是：
- 让 UI 成为更稳定的表现层接线点
- 让配置、运行态和日志入口都能以只读摘要形式进入 UI
- 为后续 Host / Config / Log / Scheduler / FA 接入预留清晰边界

---

## 它不解决什么问题

当前阶段不解决：
- 真实业务页面
- 配置编辑器
- 日志浏览系统
- 运行诊断系统
- 权限系统
- 复杂 MVVM 基础设施
- UI 插件系统
- 多进程消息桥接实现

---

## 后续接入方向

建议后续按以下顺序继续推进：
1. `UiSection` 与更多 UI 外观/布局选项的最小收敛
2. `FusionKernel` 运行摘要到 UI 状态区的默认接线
3. `FusionLog` 只读日志入口到 `LogsView` 的默认接线
4. `FusionScheduler` / `FusionFA` 的只读摘要视图接入

在这些接入过程中，仍应保持：
- UI 只消费只读投影
- UI 不承载业务真相
- UI 不假设所有后台模块与自己同进程
