# FusionApp 模块约定

## 1. 模块职责

`FusionApp` 是应用装配层，不是业务实现层。

它负责：

- 组织应用启动所需的最小选项
- 连接 `FusionKernel` 的宿主构造入口
- 接收 `FusionConfig` 与 `FusionLog` 的最小边界
- 输出面向 `FusionUI` 的只读启动摘要
- 提供应用层最小运行体入口

它不负责：

- 调度算法
- 设备控制
- 协议实现
- 数据库或持久化实现
- UI 真实页面
- 日志平台
- 配置系统

## 2. 默认装配边界

当前 `FusionApp` 的最小闭环应满足以下语义：

1. 先准备应用启动选项
2. 再准备配置与日志边界
3. 再组装宿主启动上下文
4. 再生成应用运行摘要
5. 再交由 `FusionKernel` 组装和启动宿主

应用层只负责“拼装”和“收口”，不负责实现底层能力。

## 3. 与其他模块的边界

### 与 FusionKernel

`FusionApp` 可以使用 `FusionKernel` 的宿主构造入口，但不能反向吞并宿主职责。

### 与 FusionConfig

`FusionApp` 可以接收运行根、profile 和配置边界，但不能实现配置加载框架。

### 与 FusionLog

`FusionApp` 可以接收日志 writer 与日志上下文，但不能实现日志平台。

### 与 FusionUI

`FusionApp` 应输出只读启动摘要给 `FusionUI` 使用，但不能直接实现 UI 页面逻辑。

## 4. 当前默认流程

当前默认流程为：

1. 创建 `ApplicationOptions`
2. 创建 `ApplicationBoundary`
3. 创建 `ApplicationBootstrapContext`
4. 创建 `ApplicationRuntimeDescriptor`
5. 创建 `ApplicationRuntime`
6. 由 `ApplicationRuntime` 转发初始化、启动与停止到 `FusionKernel`

## 5. 当前不做什么

当前阶段不做：

- 完整应用框架
- 多进程编排
- 动态插件系统
- 复杂依赖注入
- 业务页面承载
- 配置编辑
- 日志浏览

## 6. 后续接入方向

后续如果继续推进 `FusionApp`，优先方向应是：

- 更稳定的宿主默认启动入口
- 更明确的 UI 只读摘要接线
- 更清晰的运行态与配置边界收敛

