# FusionLog Module Conventions

## 目的

`FusionLog` 用于提供平台级日志语义、日志上下文、最小日志条目、最小 writer 以及最小配置接线能力。

第二阶段新增能力：
- `FileLoggerWriter`
- `CompositeLoggerWriter`
- `LoggingOptionsBinder`
- `DefaultLoggerWriterFactory`
- `LoggingCompositionBuilder`
- `LogFilePathResolver`

## 当前解决的问题

- 如何把一条日志写到文件
- 如何把一条日志顺序广播到多个 writer
- 如何从 `FusionConfig.LoggingSection` 映射出最小 writer 配置
- 如何形成默认 writer 组合的前半段

## 当前不解决的问题

- 完整日志框架
- 文件滚动与清理
- 异步队列
- 远程上报
- 集中式日志平台
- 监控、告警、指标、追踪系统
- 复杂 sink/pipeline

## 当前默认写入流程

1. 调用方准备 `LoggingWriterOptions`
2. `DefaultLoggerWriterFactory` 根据配置决定使用哪些 writer
3. 如需文件写入，`LogFilePathResolver` 解析输出路径
4. `DefaultLoggerWriter` 委托给具体 writer 或组合 writer
5. `LogWriteResult` 返回最小写入结论

## 文件写入职责

- `FileLoggerWriter`
  - 只负责最小文件追加写入
  - 不做滚动、压缩、清理、缓冲和异步队列
- `LogFilePathResolver`
  - 只负责把根路径、宿主、进程、模块上下文解析成文件路径
  - 不负责运行根解析和配置读取

## 组合写入职责

- `CompositeLoggerWriter`
  - 采用顺序广播
  - 聚合最小写入结果
  - 不做复杂失败恢复和路由规则

## 配置接线职责

- `LoggingOptionsBinder`
  - 只负责把 `LoggingSection` 或 `IConfigurationProvider` 映射为 `LoggingWriterOptions`
  - 不负责配置系统本身
- `DefaultLoggerWriterFactory`
  - 只负责最小显式装配
  - 不做反射扫描、自动注册或复杂 provider 管线

## 多进程约束

- 日志上下文必须显式构造和显式传递
- 不依赖静态全局上下文作为唯一真相
- 路径解析不把 `R:\` 视为唯一物理路径
- writer 组合不假设所有日志生产和消费都在同一进程

## 后续接入方向

- `FusionKernel` 可在宿主层生成 `HostLogContext`
- `FusionConfig` 提供 `LoggingSection` 和运行根相关路径
- `FusionUI` / `FusionScheduler` / `FusionFA` 可显式构造模块与进程上下文后写日志

## 当前未实现项

- 文件滚动策略
- 远程 writer
- 队列化写入
- 日志聚合平台
- 监控告警联动
