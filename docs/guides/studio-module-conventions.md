# FusionStudio 模块约定（P1）

## 1. 目标

本约定用于限制 `FusionStudio` 的实现边界，确保其作为工程工作台模块推进，而不是演变为跨层总控模块。

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

## 3. 依赖方向约定

允许依赖方向：
- `FusionStudio -> FusionApp`（应用装配摘要）
- `FusionStudio -> FusionConfig`（section 与只读配置边界）
- `FusionStudio -> FusionKernel`（宿主摘要模型）
- `FusionStudio -> FusionLog`（日志摘要模型）

禁止依赖方向：
- 不直接依赖 `FusionDomain` 业务真相对象作为 UI 唯一数据源。
- 不直接依赖 `FusionScheduler` 或 `FusionFA` 的内部实现对象。
- 不引入数据库、ORM、消息中间件依赖。

## 4. 只读接线约定

`FusionStudio` 仅消费以下类型输入：
- Bootstrap Context
- Runtime Summary
- Log Summary
- Config Mapping Result

禁止：
- 在 `ViewModel` 中直接执行调度控制、设备控制、协议交互。
- 在 `ViewModel` 中直接写配置与日志基础设施。

## 5. E95 对齐约定

`FusionStudio` 为工程工作台，不替代 `FusionUI` 的运行 HMI。  
其信息组织应遵循“工程入口清晰、运行入口只读、调试入口隔离”的原则。

建议优先入口：
- 工程配置
- 日志详细入口
- 运行诊断
- 调试助手
- 模块状态

## 6. 当前阶段不做项

当前阶段（P1）明确不做：
- 真实配置编辑器
- 真实日志检索引擎
- 真实调试执行工具
- 复杂页面框架与插件系统
- 权限系统、国际化、复杂主题系统

## 7. 后续进入 P2 的实现边界

P2 可做：
- 最小 Shell
- 最小导航
- 最小占位页面
- 只读摘要展示
- 最小组合入口

P2 仍不做：
- 业务逻辑
- 调度算法
- 协议实现
- 数据存储实现
