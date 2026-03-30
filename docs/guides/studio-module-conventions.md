# FusionStudio 模块约定

## 1. 目标

本文用于约束 `FusionStudio` 的实现边界，确保它作为工程工作台推进，而不是演变成跨层总控模块。

## 2. 目录与命名约定

建议目录：

- `Shell/`
- `Navigation/`
- `Views/`
- `ViewModels/`
- `Composition/`
- `Models/`
- `Projections/`

命名空间与目录保持一致，例如：

- `FusionStudio.Shell`
- `FusionStudio.Navigation`
- `FusionStudio.Composition`
- `FusionStudio.Models`

## 3. 依赖方向约定

允许依赖：

- `FusionStudio -> FusionApp`
- `FusionStudio -> FusionConfig`
- `FusionStudio -> FusionKernel`
- `FusionStudio -> FusionLog`

禁止依赖：

- 不直接依赖 `FusionDomain` 内部真相对象作为唯一数据源
- 不直接依赖 `FusionScheduler` 或 `FusionFA` 的内部实现对象
- 不引入数据库、ORM、消息中间件依赖

## 4. 信息组织约定

`FusionStudio` 不按 E95 HMI 风格组织界面，而按工程对象和工具域组织：

- 设备总览
- 工程配置
- 报警配置
- 互锁管理
- 模块工作台
- IO 监控
- 运行诊断
- 详细日志
- 工程控制台
- 调试助手

首页应优先展示设备总览与工程树，而不是普通菜单页。

## 5. 接线约定

`FusionStudio` 只消费以下只读输入：

- Bootstrap Context
- Runtime Summary
- Module Summary
- Configuration Summary
- Log Summary

禁止：

- 在 `ViewModel` 中直接执行设备控制
- 在 `ViewModel` 中直接写配置和日志基础设施
- 依赖全局静态状态作为唯一真相

## 6. 工程树约定

工程树是 `FusionStudio` 的核心组织结构之一，至少应表达：

- 设备根节点
- 模块节点
- 模块工具节点

工具节点优先使用稳定工具域：

- 参数
- IO
- 报警
- 互锁
- 状态
- 调试

后续如需扩展更多工程工具，应优先扩展工具节点，而不是直接增加新的跨层页面。

## 7. 当前阶段不做什么

当前阶段明确不做：

- 真实配置编辑器
- 日志检索与过滤引擎
- 调试命令执行器
- 复杂插件系统
- 权限系统
- 多语言系统
- 主题系统

## 8. 扩展原则

`FusionStudio` 允许扩展，但扩展点应保持轻量和显式：

- 优先按工具域扩展
- 优先按模块工作页扩展
- 不提前构建重型插件框架
- 不把工具入口做成跨层万能调用口

## 9. 工具页上下文约定

当前阶段的工程工具页应共享统一的只读模块上下文，而不是各自重新拼装摘要。

推荐最小约定：

- 模块上下文负责承载：
  - 模块标识
  - 模块名称
  - 模块类型
  - 模块状态
  - 运行 Profile
  - Runtime Root
- 工具页上下文负责承载：
  - 当前设备名称
  - 当前工具域
  - 当前模块上下文
  - 当前上下文来源摘要

这样做的目的：

- 让报警、互锁、IO、控制台等页面使用一致的上下文头
- 让后续工程工具继续复用同一套上下文模型
- 避免每个页面单独依赖更深层的运行时对象
