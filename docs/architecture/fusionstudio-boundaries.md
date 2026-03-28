# FusionStudio 模块边界说明（P1）

## 1. 模块定位

`FusionStudio` 是面向开发工程师、现场调试工程师、平台支持工程师的工程工作台模块。  
它属于 GUI 表现层模块，但不等同于设备运行操作界面。

`FusionStudio` 负责：
- 工程配置入口的承载边界
- 详细日志入口的承载边界
- 运行诊断摘要入口的承载边界
- 调试助手入口的承载边界
- 工程视角的信息组织与导航

`FusionStudio` 不负责：
- 业务真相定义
- 调度算法
- 自动化协议实现
- 设备控制实现
- 配置系统实现
- 日志系统实现
- 宿主运行时实现

## 2. 与 FusionUI 的职责边界

`FusionUI` 与 `FusionStudio` 都是 GUI 模块，但角色不同：

- `FusionUI`：面向设备操作与运行监控，偏 E95 HMI 运行界面。
- `FusionStudio`：面向工程与调试支持，偏工程工作台与诊断入口。

边界原则：
- 不在 `FusionUI` 中堆叠工程调试工作台能力。
- 不在 `FusionStudio` 中实现设备运行主流程。
- 两者都只消费只读摘要与投影，不接管业务真相。

## 3. 与其他模块的边界

### 3.1 与 FusionKernel

- `FusionStudio` 可消费宿主状态摘要、模块状态摘要、运行实例摘要。
- `FusionStudio` 不实现宿主生命周期控制逻辑，不替代 `FusionKernel`。

### 3.2 与 FusionConfig

- `FusionStudio` 可消费 `UiSection` 或 Studio 专用 section 的只读映射结果。
- `FusionStudio` 不实现配置加载、配置源组合、配置写入与配置系统策略。

### 3.3 与 FusionLog

- `FusionStudio` 可消费日志摘要、日志入口描述。
- `FusionStudio` 不实现日志 writer、日志落盘、日志传输、日志平台能力。

### 3.4 与 FusionApp

- `FusionStudio` 由 `FusionApp` 负责装配接入。
- `FusionStudio` 不承担应用层总装配职责。

### 3.5 与 FusionDomain / FusionScheduler / FusionFA

- `FusionStudio` 只消费它们对外提供的只读摘要或投影。
- `FusionStudio` 不直接操作领域真相对象，不直接实现调度与自动化逻辑。

## 4. 当前阶段（P1）做与不做

当前阶段做：
- 模块职责定义
- 信息架构定义
- 接线边界定义
- 术语与约定收敛
- 最小项目骨架（仅壳层与占位）

当前阶段不做：
- 真实业务页面实现
- 配置编辑器实现
- 日志检索系统实现
- 调试工具真实执行逻辑
- 任何协议、数据库、驱动、中间件接入

## 5. 进入 P2 的最小方向

进入 P2 时，建议最小推进顺序：
1. 建立 `FusionStudio` WPF Shell 与导航骨架。
2. 接入 `FusionApp` 输出的只读 bootstrap 摘要。
3. 形成配置入口、日志入口、运行诊断入口的只读占位页面。
4. 保持只读消费模型，不引入业务控制逻辑。
