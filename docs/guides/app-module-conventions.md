# FusionApp 模块约定

## 1. 模块职责

`FusionApp` 是应用装配层，不是业务实现层。它负责把已经存在的底座能力收口成一个可启动、可转发、可观测的默认入口。

它负责：
- 组织应用启动顺序
- 连接 `FusionKernel` 的宿主边界
- 接入 `FusionConfig` 的最小配置边界
- 接入 `FusionLog` 的最小日志边界
- 生成面向 `FusionUI` 的只读启动摘要
- 输出应用运行体和应用装配结果

它不负责：
- 调度算法
- 设备控制
- 协议实现
- 数据库或持久化实现
- 业务页面逻辑
- 配置系统本体
- 日志平台本体

## 2. 默认装配闭环

当前 `FusionApp` 的默认闭环为：

1. 创建 `ApplicationOptions`
2. 创建 `ApplicationBoundary`
3. 解析运行根与 profile
4. 创建 `ApplicationBootstrapContext`
5. 创建 `ApplicationRuntimeDescriptor`
6. 创建 `ApplicationUiBootstrapDescriptor`
7. 创建 `UiBootstrapContext`
8. 创建 `UiRuntimeDescriptor`
9. 创建 `ApplicationStudioBootstrapDescriptor`
10. 创建 `ApplicationAssembly`
11. 创建 `ApplicationRuntime`
12. 通过 `ApplicationRuntime` 转发宿主初始化、启动与停止

其中：
- `ApplicationBoundary` 承载配置、日志的外部接线边界
- `ApplicationBootstrapContext` 承载应用启动时的最小上下文
- `ApplicationAssembly` 承载一次完整默认装配的结果
- `ApplicationRuntime` 只负责把宿主流程转发出去，不接管业务真相
- `ApplicationUiBootstrapDescriptor` 与 `ApplicationStudioBootstrapDescriptor` 分别面向运行 HMI 和工程工作台输出只读启动摘要

## 3. 与其他模块的边界

### 与 `FusionKernel`

`FusionApp` 可以调用 `FusionKernel` 的宿主构造、初始化、启动和停止接口，但不能反向吞并宿主职责。

### 与 `FusionConfig`

`FusionApp` 可以从配置 provider 或 snapshot 中读取运行根、profile 和 UI 相关配置节，但不能实现配置系统本体。

### 与 `FusionLog`

`FusionApp` 可以接收日志 writer 与日志上下文，并把它们作为应用边界的一部分传递下去，但不能实现日志平台本体。

### 与 `FusionUI`

`FusionApp` 只输出 UI 侧可消费的只读启动摘要和运行态摘要，不直接承载业务页面逻辑。

### 与 `FusionStudio`

`FusionApp` 只输出 Studio 侧可消费的只读启动摘要和运行态摘要，不直接承载工程配置器、日志浏览器或调试助手逻辑。

## 4. 当前不做什么

当前阶段不做：
- 完整应用框架
- 多进程编排
- 复杂 DI
- 动态插件
- IPC
- 业务控制
- 调度控制
- 配置编辑
- 日志浏览系统

## 5. 后续方向

后续如果继续推进 `FusionApp`，优先方向是：
- 更稳定的默认启动入口
- 更清晰的应用运行态摘要
- 更明确的 UI / Config / Log 接线细化
- 仍然不进入业务实现
