# FusionStudio 模块边界说明

## 1. 模块定位

`FusionStudio` 是面向设备厂家、开发工程师、现场调试工程师的平台工程工作台。

它属于 GUI 表现层模块，但不等同于车间运行 HMI。`FusionStudio` 的主要目标是提升工程构建、模块配置、设备调试、运行诊断和问题定位的效率。

`FusionStudio` 负责：
- 设备总览与模块树入口
- 工程配置入口
- 报警配置入口
- 互锁管理入口
- IO 监控入口
- 运行诊断入口
- 详细日志入口
- 工程控制与调试助手入口

`FusionStudio` 不负责：
- 设备生产运行主界面
- 业务真相定义
- 调度算法
- 自动化协议实现
- 配置系统实现
- 日志系统实现
- 宿主运行时实现
- 设备控制驱动实现

## 2. 与 FusionUI 的边界

`FusionUI` 与 `FusionStudio` 都是 GUI 模块，但目标用户和信息组织方式不同。

- `FusionUI`：面向车间操作人员与设备运行人员，遵循 E95 对齐方向，强调运行操作、受控交互和生产过程展示。
- `FusionStudio`：面向开发与调试人员，不要求遵循 E95 HMI 风格，强调工程效率、配置效率、调试效率和诊断效率。

边界原则：
- 不在 `FusionUI` 中继续堆叠工程配置器、日志工作台和调试工作台能力。
- 不在 `FusionStudio` 中实现车间操作主流程。
- 两者都只消费只读摘要或明确的接线结果，不接管业务真相。

## 3. 与其他模块的边界

### 3.1 与 FusionKernel

- `FusionStudio` 只消费宿主摘要、运行时摘要、模块摘要。
- `FusionStudio` 不实现宿主生命周期控制，不替代 `FusionKernel`。

### 3.2 与 FusionConfig

- `FusionStudio` 可以消费工程配置摘要、配置映射结果、配置入口描述。
- `FusionStudio` 不实现配置加载、配置合并、配置保存和配置系统策略。

### 3.3 与 FusionLog

- `FusionStudio` 可以消费日志入口摘要、日志摘要集合、日志来源描述。
- `FusionStudio` 不实现 writer、落盘、检索引擎和日志平台能力。

### 3.4 与 FusionApp

- `FusionStudio` 由 `FusionApp` 负责装配接入。
- `FusionStudio` 不承担应用层总装配职责。

### 3.5 与 FusionDomain / FusionScheduler / FusionFA

- `FusionStudio` 只能消费这些模块提供的只读摘要、投影或接线结果。
- `FusionStudio` 不直接操作领域真相对象，不直接实现调度、自动化和协议逻辑。

## 4. 当前阶段做与不做

当前阶段做：
- 工程工作台的信息架构
- 设备总览与模块树骨架
- 工程工具域导航骨架
- 只读摘要模型
- 最小组合入口

当前阶段不做：
- 真实配置编辑器
- 真实日志浏览系统
- 真实调试执行器
- 模块控制指令执行器
- 协议、数据库、驱动、中间件接入

## 5. 后续进入下一阶段的方向

进入下一阶段后，建议按以下顺序推进：
1. 接入 `FusionApp` 的默认 bootstrap 结果。
2. 接入 `FusionKernel` 的宿主与模块只读摘要。
3. 接入 `FusionConfig` 的工程配置摘要。
4. 接入 `FusionLog` 的详细日志入口摘要。
5. 继续保持只读消费，控制动作通过明确边界进入后台模块。
