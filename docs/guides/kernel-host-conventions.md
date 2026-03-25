# FusionKernel 宿主约定

## 目的

本文件用于说明 `FusionKernel` 当前阶段新增的最小宿主初始化与启动协调骨架。

当前阶段目标：
- 让宿主具备更明确的构造、初始化、启动、停止闭环
- 让模块注册与生命周期协调具有清晰边界
- 为 `FusionConfig`、`FusionLog`、`FusionUI` 后续接线提供更稳定的承载点

---

## 当前新增能力

当前 `FusionKernel` 已新增或补强：
- `HostDescriptor`
- `HostState`
- `HostInitializationState`
- `RuntimeDescriptor`
- `RuntimeInstanceId`
- `RuntimeStatus`
- `RuntimeContext`
- `IModuleLifecycle`
- `ModuleState`
- `ModuleInitializationContext`
- `ModuleStartContext`
- `ModuleStopContext`
- `HostCompositionRoot`
- `HostRuntimeBuilder`
- `HostBootstrapContext`
- `HostDiagnosticInfo`
- `HostStopResult`
- `ModuleInitializationResult`
- `ModuleStartResult`
- `ModuleStopResult`

---

## Host 的职责

`FusionKernel` 中的 Host 只负责：
- 承载运行时上下文
- 承载模块集合
- 协调最小初始化、启动、停止顺序
- 暴露最小诊断结果

`Host` 不负责：
- 业务逻辑
- 调度逻辑
- 配置系统实现
- 日志平台实现
- IPC
- 动态插件发现
- 完整 DI 容器

---

## RuntimeContext 的职责

`RuntimeContext` 当前用于表达：
- 运行实例标识
- runtime root
- run mode
- profile
- 当前 runtime status
- Config / Log 的最小边界入口引用

当前只保存最小引用入口，不在 Kernel 内实现 Config 或 Log。

---

## 模块生命周期协调

当前最小生命周期阶段：
1. 注册
2. 初始化
3. 启动
4. 停止

默认策略是：
- 顺序初始化
- 顺序启动
- 逆序停止

当前不实现：
- 复杂依赖求解
- 并行启动编排
- 恢复策略
- 后台服务调度

---

## 默认组合入口

当前最小组合入口为：
- `HostCompositionRoot`
- `HostRuntimeBuilder`

它们解决的是：
- 用显式方式组装宿主
- 显式传入 Config / Log 边界
- 显式添加模块
- 构造最小默认 Host

它们不解决：
- 自动扫描
- 自动注册
- 动态发现
- 复杂宿主框架

---

## 当前默认流程

当前默认流程为：
1. 准备 `HostCompositionOptions`
2. 准备 `HostBootstrapContext`
3. 通过 `HostRuntimeBuilder` 添加模块
4. 构造 Host
5. 执行初始化
6. 执行启动
7. 执行停止
8. 读取最小诊断信息

---

## 当前未实现项

当前仍未实现：
- 多进程代理实现
- IPC 接线
- 完整宿主运行时
- 复杂诊断系统
- 健康检查平台
- 完整依赖注入系统
- 插件系统

---

## 后续接入方向

后续建议按以下方向继续：
1. 让 `FusionConfig` 通过稳定边界进入 Host
2. 让 `FusionLog` 通过稳定边界进入 Host
3. 让 `FusionUI` 通过 Host 读取最小运行摘要
4. 再逐步考虑 `FusionApp`、`FusionScheduler`、`FusionFA` 的宿主接线
