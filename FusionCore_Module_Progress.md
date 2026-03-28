# FusionCore 模块进展盘点

## 说明

本文用于记录当前仓库代码、测试和模块骨架完成度下，各模块的阶段判断。

阶段含义采用当前治理语境下的简化表达：

- `P1`：仅有项目占位或极薄骨架
- `P2`：核心语义、目录结构、最小契约与最小测试已经形成
- `P2→P3`：在 `P2` 基础上，已经开始形成最小默认实现或最小接线闭环
- `P3`：具备相对稳定的最小可接线能力，但仍未进入复杂运行时或完整基础设施

本文是阶段盘点，不代表最终产品完成度。

## 当前总览

| 模块 | 当前阶段 | 说明 |
| --- | --- | --- |
| FusionKernel | P2→P3 | 已形成宿主、模块、生命周期、运行时上下文、最小接线与最小初始化/启动协调闭环 |
| FusionConfig | P2→P3 | 已形成配置语义、Section Key、Snapshot、Provider、Composite Source 与最小默认加载闭环 |
| FusionLog | P2→P3 前半段 | 已形成日志语义、上下文、条目、writer、文件写入骨架与最小配置接线前半段 |
| FusionUI | P2 | 已形成 WPF Shell、导航骨架、只读摘要入口和最小 UI 接线边界 |
| FusionDomain | P2 | 已形成领域真相对象、值对象、枚举与具体领域事件目录骨架 |
| FusionEquipment.Abstractions | P2 | 已形成模块抽象、生命周期抽象、运行上下文与能力边界 |
| FusionScheduler | P2 | 已形成调度协作契约、事件消费/编排/评估/计划/协调骨架 |
| FusionFA | P2 | 已形成自动化映射契约、只读视图模型与最小命令/查询骨架 |
| FusionApp | P1 | 仍主要是应用层占位，尚未系统展开应用服务与接线骨架 |
| FusionData | P1 | 当前治理阶段只允许规划层，不进入实现；已有占位代码不代表阶段提升 |

## 模块说明

### FusionKernel

当前已具备：

- Host 与 Runtime 的最小骨架
- 模块注册与生命周期协调骨架
- Host 初始化、启动、停止的最小闭环
- Config / Log 的最小接线入口

仍需后续收敛：

- 更细的宿主默认装配入口
- 更明确的本地实现与未来代理实现分层

### FusionConfig

当前已具备：

- 配置抽象与配置节
- Runtime Root 与 Profile 语义
- Snapshot / Provider / Composite Source
- 最小默认加载边界

仍需后续收敛：

- 更完整的宿主接线入口
- 真实文件加载适配，但仍不进入热加载或远程配置

### FusionLog

当前已具备：

- 日志级别、分类、上下文、条目
- Memory / Null / File writer
- Composite writer
- 最小配置映射与默认 writer 组合

仍需后续收敛：

- 更清晰的 runtime root 路径接线
- 更明确的 Host / Config / Log 默认装配边界

### FusionUI

当前已具备：

- WPF Shell
- 导航骨架
- 四区式布局
- 运行态摘要入口
- 日志摘要入口

仍需后续收敛：

- 更完整的 Config / Log / Runtime 接线
- 更细的 E95 信息架构收敛

### FusionDomain

当前已具备：

- 核心实体、值对象、枚举
- 具体领域事件目录骨架

仍需后续收敛：

- 更细粒度的领域语义沉淀

### FusionScheduler

当前已具备：

- 调度事件消费契约
- 评估、计划、协调的最小协作边界
- 与 Domain 事件的对齐骨架

仍需后续收敛：

- 更明确的执行前协调闭环

### FusionFA

当前已具备：

- 面向外部自动化的映射契约
- 只读视图模型与命令/查询骨架

仍需后续收敛：

- 更细的外部投影分层

### FusionApp

当前主要仍是占位。

建议后续在 `FusionKernel`、`FusionConfig`、`FusionLog`、`FusionUI` 的稳定边界上，逐步展开应用服务装配。

### FusionData

当前治理阶段为 `P1`，只允许停留在规划层。

已有代码占位并不意味着阶段提升；建议等进入 `P2` 再继续使用。

## 当前治理结论

当前仓库主线已经完成平台底座的主要骨架收敛：

- `FusionKernel`、`FusionConfig`、`FusionLog`、`FusionUI` 处于可继续接线状态
- `FusionDomain`、`FusionScheduler`、`FusionFA`、`FusionEquipment.Abstractions` 已具有较稳定语义骨架
- `FusionData` 不应在当前阶段推进到 `P2`
- `FusionApp` 是当前仍最薄弱的模块之一

## 下一步建议

建议下一阶段优先顺序如下：

1. `FusionApp`
2. `FusionKernel` 与 `FusionConfig` 的默认接线闭环整理
3. `FusionLog` 的更明确运行根接线
4. `FusionUI` 的 Config / Log / Runtime 默认接线

## 备注

本文是当前仓库的阶段盘点快照。
后续如果治理阶段发生变化，应同步更新本文。
