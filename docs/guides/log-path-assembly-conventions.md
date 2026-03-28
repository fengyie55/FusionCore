# FusionLog 默认日志路径装配约定

## 目的

本文档说明 `FusionLog` 当前阶段如何把 `FusionConfig` 提供的运行根语义，稳定映射为 `FileLoggerWriter` 的默认日志路径。

## 默认装配链路

当前默认链路如下：

1. `FusionConfig` 提供 `RuntimeRootOptions` 与 `RuntimePathSet`
2. `FusionConfig.LoggingSection` 提供日志启用开关与日志目录语义
3. `FusionLog.LoggingOptionsBinder` 读取配置提供者或配置节
4. `LoggingOptionsBinder` 生成 `LoggingWriterOptions`
5. `DefaultLoggerWriterFactory` 根据选项构造默认 writer
6. `FileLoggerWriter` 在解析后的日志根目录下，再按宿主 / 进程 / 模块上下文追加子目录

## 默认规则

- 如果 `LoggingSection.LogsPath` 是绝对路径，则直接使用该路径。
- 如果 `LoggingSection.LogsPath` 是相对路径，则按 `RuntimeRootOptions.PathSet.LogsPath` 下的子目录处理。
- 如果 `LoggingSection.LogsPath` 为空或空白，则回退到 `RuntimeRootOptions.PathSet.LogsPath`。
- `FileLoggerWriter` 不负责决定日志根目录，只负责在给定根目录下执行最小追加写入。
- `R:\` 只是默认逻辑运行根，不是唯一物理路径。

## 作用边界

本文档只解决“默认日志路径如何从配置和运行根落到文件 writer”。

它不解决：

- 文件滚动
- 压缩
- 清理
- 异步队列
- 远程上报
- 监控告警
- 集中式日志平台

## 后续接入

后续如果 `Host`、`UI`、`Scheduler` 或 `FA` 需要默认日志路径，只应显式传入配置快照、运行根和日志上下文，再由这条装配链路生成 writer。
