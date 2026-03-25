# FusionConfig Module Conventions

## 目的

本文件用于说明 `FusionConfig` 第二阶段新增的默认接线骨架，以及当前阶段配置读取的最小闭环。

它解决的问题：
- 平台如何稳定标识配置节
- 一次配置加载如何形成只读快照
- Host / Log / UI / Scheduler / FA 如何通过统一读取边界获取自己的配置节
- 多个简单来源如何以最小规则组合

它不解决的问题：
- 完整配置框架
- 热加载
- 文件监听
- 远程配置
- 数据库配置中心
- 复杂 override / 优先级引擎

## 当前核心类型

- `ConfigurationSectionKey`
  - 用于表达配置节的稳定身份
  - 配置节读取不再依赖裸字符串到处漂移
- `ConfigurationSnapshot`
  - 表示一次加载后的只读配置视图
  - 持有当前 `Profile`、`RuntimeRoot` 与配置节集合
- `DefaultConfigurationProvider`
  - 对外提供最小读取边界
  - 支持读取当前 profile、runtime root、指定 section
- `CompositeConfigurationSource`
  - 提供最小组合来源能力
  - 当前规则是“按顺序读取，先命中的 section 保留”
- `DefaultConfigurationLoader`
  - 执行最小加载流程
  - 读取来源、形成快照、收敛最小校验结果

## 当前默认加载流程

1. 调用方准备一个或多个 `IConfigurationSource`
2. 调用 `DefaultConfigurationLoader.Load(...)`
3. Loader 顺序读取来源中的配置节
4. Loader 形成 `ConfigurationSnapshot`
5. Loader 返回 `ConfigurationLoadResult`
6. 调用方可将 `Snapshot` 包装为 `DefaultConfigurationProvider`

## 组合来源规则

当前只支持最小组合规则：
- 来源按给定顺序处理
- 同一个 `ConfigurationSectionKey` 只保留第一次命中的节
- 不实现复杂合并树
- 不实现深层字段级 override

这条规则的目标是保持透明、稳定、可测试。

## 运行根约定

- 默认逻辑运行根仍可表达为 `R:\`
- 但代码不把 `R:\` 视为唯一物理路径
- `RuntimeRootOptions` 同时表达逻辑根与物理根
- `RuntimePathSet` 提供 `config / data / logs / runtime / temp / backups / deploy / scripts` 的最小派生目录语义

## 当前未实现项

- 真实文件解析器
- 配置模板实例化工具
- 热更新与监听器
- 分层 override 规则
- 配置校验器注册中心
- Host 自动接线

## 后续接入方向

- `FusionKernel` 可在宿主初始化阶段创建 `ConfigurationSnapshot`
- `FusionLog` 可读取 `LoggingSection`
- `FusionUI` 可读取 `UiSection`
- `FusionScheduler` 可读取 `SchedulerSection`
- `FusionFA` 可读取 `FactoryAutomationSection`
- 设备宿主或设备模块可读取 `EquipmentSection`

当前阶段建议始终通过 `IConfigurationProvider` 或 `IConfigurationSnapshot` 消费配置，而不是通过全局静态配置对象共享真相。
