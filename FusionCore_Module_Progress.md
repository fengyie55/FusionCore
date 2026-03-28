# FusionCore 模块进展盘点

## 说明

本文档用于记录当前仓库内各模块在治理阶段上的阶段判断，便于后续任务统一参考。

阶段含义：

- `P1`：仅有占位或极薄规划骨架
- `P2`：已形成核心语义、目录结构、最小契约与最小测试
- `P2→P3`：在 `P2` 基础上，已经开始形成最小默认实现或最小接线闭环
- `P3`：具备相对稳定的最小可接线能力，但仍未进入复杂运行时或完整基础设施

## 当前总览

| 模块 | 当前阶段 | 说明 |
| --- | --- | --- |
| FusionKernel | P2→P3 | 已形成宿主、运行时、模块生命周期、最小默认初始化与启动闭环 |
| FusionConfig | P2→P3 | 已形成配置节、Snapshot、Provider、Composite Source 与最小默认接线 |
| FusionLog | P2→P3 前半段 | 已形成日志语义、上下文、writer、文件写入骨架与配置接线前半段 |
| FusionUI | P2 | 已形成 WPF Shell、导航骨架、状态摘要与最小 UI 接线入口 |
| FusionDomain | P2 | 已形成领域真相对象、值对象、枚举与领域事件目录骨架 |
| FusionEquipment.Abstractions | P2 | 已形成模块抽象、生命周期抽象与运行上文下文语义边界 |
| FusionScheduler | P2 | 已形成调度协作契约、评估/计划/协调骨架 |
| FusionFA | P2 | 已形成自动化映射契约、只读视图模型与最小命令查询骨架 |
| FusionApp | P2 | 已形成应用装配骨架、最小 bootstrap 入口与 Host / Config / Log / UI 摘要接线边界 |
| FusionData | P1 | 仍停留在规划层，仅定义数据管理与存储边界，不进入真实实现 |

## 各模块简述

### FusionKernel

当前已具备：

- 宿主与运行时最小骨架
- 模块注册与生命周期协调骨架
- Host 初始化、启动、停止的最小闭环
- Config / Log 的最小接线入口

仍需继续收敛：

- 本地实现与未来代理实现之间的更清晰分层
- 更细的宿主默认装配入口

### FusionConfig

当前已具备：

- 配置抽象与配置节边界
- Runtime Root 与 Profile 语义
- Snapshot / Provider / Composite Source
- 最小默认加载与校验收敛

仍需继续收敛：

- 更完整的宿主接线入口
- 更明确的配置读取与运行根路径说明

### FusionLog

当前已具备：

- 日志级别、分类、上下文、条目
- Memory / Null / File writer
- Composite writer
- 最小配置映射与默认 writer 组合

仍需继续收敛：

- 更明确的 runtime root 路径接线
- 更明确的 Host / Config / Log 默认装配边界

### FusionUI

当前已具备：

- WPF Shell
- 导航骨架
- 状态展示与摘要入口
- 运行态摘要只读模型
- 日志摘要只读模型
- `UiBootstrapContext` 与 `UiCompositionRoot` 的最小接线入口

仍需继续收敛：

- 更完整的 Config / Log / Runtime 接线
- 更细的 E95 信息架构收敛

### FusionDomain

当前已具备：

- 核心实体、值对象、枚举
- 具体领域事件目录骨架

仍需继续收敛：

- 更细粒度的领域语义沉淀

### FusionScheduler

当前已具备：

- 调度事件消费契约
- 评估、计划、协调的最小协作边界
- 与 Domain 事件的对齐骨架

仍需继续收敛：

- 更明确的执行前协调闭环

### FusionFA

当前已具备：

- 面向外部自动化的映射契约
- 只读视图模型与命令查询骨架

仍需继续收敛：

- 更细的外部投影分层

### FusionApp

当前已具备：

- 应用装配边界
- 最小 bootstrap / composition 入口
- Host / Config / Log 的最小接线边界
- 面向 UI 的只读启动摘要

仍需继续收敛：

- 更明确的应用默认启动入口
- 与 UI 的更稳定只读接线

### FusionData

当前仍处于 `P1`，只允许停留在规划层。

已有占位代码不代表阶段提升；建议等进入 `P2` 后再继续使用。

## 当前治理结论

当前仓库主线已经完成平台底座的大部分骨架收敛：

- `FusionKernel`、`FusionConfig`、`FusionLog`、`FusionUI`、`FusionApp` 均已形成可继续接线的最小骨架
- `FusionDomain`、`FusionScheduler`、`FusionFA`、`FusionEquipment.Abstractions` 均已具备稳定语义边界
- `FusionData` 仍应保持在 `P1`

## 下一步建议

建议后续优先顺序：

1. `FusionApp`
2. `FusionKernel` 与 `FusionConfig` 的默认接线收口
3. `FusionLog` 的更明确运行根接线
4. `FusionUI` 的 Config / Log / Runtime 默认接线

## 备注

本文件是当前仓库的阶段盘点快照。
如治理阶段发生变化，应同步更新本文档。
