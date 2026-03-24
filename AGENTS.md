# AGENTS.md

## Project
FusionCore 是基于 WPF 和 .NET 构建的模块化半导体设备控制平台。  
该项目采用架构驱动的开发模式，在实现前需遵循已批准的设计文档。  
FusionCore 的目标不是构建单体上位机程序，而是逐步演进为多进程协作的半导体设备软件平台。

---

## Current Phase (当前阶段)
当前处于 **Phase 1：架构骨架与语义基线建立阶段**。

当前已完成/正在推进的工作包括：
- 解决方案与项目骨架初始化
- FusionDomain 基础领域对象模型
- FusionEquipment.Abstractions 模块抽象
- FusionScheduler 调度协作契约骨架
- FusionFA 工厂自动化映射契约骨架
- FusionDomain 具体领域事件目录骨架
- 多进程拓扑与进程间协作设计
- 运行时目录与文件布局设计
- 构建、发布与运行脚本规范
- SEMI 标准映射、术语表与一期范围文档

当前阶段目标：
- 保持解决方案结构清晰稳定
- 保持模块职责边界稳定
- 建立可演进的领域语义、调度语义、FA 投影语义
- 保证解决方案可编译、可测试
- 避免过早进入复杂协议实现、算法实现和基础设施实现

---

## Required Reading Before Changes (变更前必读文档)
修改代码前必须阅读以下内容：

1. `docs/architecture/` 目录下的正式设计文档
2. `implementation-rules.md`
3. `project-structure.md`
4. `docs/standards/` 目录下的 Markdown 文件
5. `docs/guides/` 目录下的 Markdown 文件

优先级顺序：
1. 架构文档
2. 实现规则
3. 标准摘要与范围文档
4. 工程指南（guides）
5. 项目结构文档
6. 当前任务需求文档

如有冲突：
- 架构文档优先于实现便利性
- 仓库内 `docs/standards/*.md` 优先于未明确确认的标准假设
- `docs/guides/*.md` 用于约束实现方式
- `implementation-rules.md` 优先于临时编码习惯

---

## Architecture Baseline (架构基线)
以下文档属于 FusionCore 当前正式架构基线，应优先遵循：

- `docs/architecture/FusionCore_Architecture_Baseline_v1_0.docx`
- `docs/architecture/FusionDomain_DomainModel_Design_v1_1_Compatibility.docx`
- `docs/architecture/FusionScheduler_Scheduling_and_Material_Flow_Design_v1_0.docx`
- `docs/architecture/FusionFA_Factory_Automation_Interface_Design_v1_0.docx`
- `docs/architecture/FusionUI_E95_Aligned_HMI_Design_v1_0.docx`
- `docs/architecture/FusionCore_Process_Topology_and_IPC_Design_v1_0.docx`
- `docs/architecture/FusionCore_Runtime_Directory_and_File_Layout_Design_v1_0.docx`
- `docs/architecture/FusionCore_Build_Publish and Runtime Script Specification_V1_1.docx`

如果某个实现思路与上述文档冲突，应先报告冲突，不得擅自改变架构方向。

---

## Hard Constraints (强制约束)
- 不得违反层边界规则。
- 不得在 FusionUI 中放置业务逻辑。
- 不得让 FusionDomain 依赖 WPF、数据库、网络、消息总线或协议实现类库。
- 不得让 FusionScheduler 直接控制 IO、运动控制、通信驱动或协议驱动。
- 不得让 FusionFA 实现内部设备调度逻辑。
- 不得在 `FusionEquipment.Abstractions` 中放置 PM/TM/CM/BM 的具体实现。
- 不得在当前阶段引入 SECS/GEM/HSMS 的具体通信实现。
- 所有代码注释（包括单行注释、多行注释、XML 文档注释）必须使用中文编写，禁止使用英文注释。

---

## Multi-Process Constraints (多进程架构约束)
FusionCore 按多进程架构方向设计，所有实现必须默认未来可能跨进程部署。

必须遵守：
- 不得默认所有模块交互都是同进程方法调用。
- 不得假设所有依赖对象共享同一地址空间。
- 不得使用全局可变静态对象保存业务真相。
- 不得把进程内共享对象视为唯一真相来源。
- 所有契约设计必须尽量保持可跨进程序列化、可代理、可替换为 IPC/RPC。
- DomainEvent 是领域事实，不自动等价于跨进程消息。
- 如需跨进程通信，应单独定义 IPC / Integration Message 契约。
- 输入输出模型优先使用轻量对象、显式 Id、显式状态，避免深层可变对象图。

实现时还应遵循：
- `docs/guides/multiprocess-implementation-guardrails.md`

---

## Runtime and Script Conventions (运行时与脚本约束)
实现涉及路径、配置、日志、数据库、脚本时，必须遵循：

- `docs/guides/runtime-path-conventions.md`
- `docs/guides/build-and-publish-conventions.md`

关键规则：
- 运行时目录与源码仓库目录必须分离
- `R:\` 是默认逻辑运行根，但不得在代码中硬编码为唯一前提
- 日志、temp、runtime 文件需区分共享与进程私有
- 构建、发布、初始化、运行脚本职责不得混乱
- 不得把真实运行数据提交进仓库

---

## Standards Guidance (标准资料使用约束)
- 优先依据 `docs/standards/*.md` 实施。
- `docs/standards/raw/` 下的文件仅可作为本地参考，不得作为直接可提交内容来源。
- 不要把保密或受许可限制的标准原文复制到代码、注释或仓库文档中。
- 如需更细标准约束，应先人工整理成可提交的 Markdown 摘要，再据此实现。

---

## Phase-1 Restrictions (第一阶段限制)
在第一阶段，禁止：
- 实现完整的 SECS/GEM 协议行为
- 实现 HSMS 会话与报文传输
- 实现生产调度算法
- 实现恢复策略算法
- 引入数据库持久化耦合
- 添加不必要的第三方框架
- 为图方便创建跨层快捷调用
- 引入消息中间件、事件总线、Outbox、分布式基础设施
- 提前实现 PM/TM/CM/BM 的具体模块逻辑

第一阶段允许：
- 骨架
- 契约
- 领域对象
- 事件目录
- 视图与映射模型
- 最小测试
- 面向多进程演进的友好约束

---

## Working Style (工作规范)
每个任务执行流程：
1. 总结实现计划
2. 列出待创建/修改的文件
3. 做最小化、整洁的代码变更
4. 运行验证命令
5. 清晰报告结果

补充要求：
- 优先最小改动，不要无故重构既有骨架
- 优先复用已有 FusionDomain 语义
- 优先保持目录、命名空间和职责一致
- 若发现当前任务与架构文档冲突，应先说明冲突，不要擅自决定新架构

---

## Validation (验证流程)
代码变更后，按需运行以下命令：
- `dotnet restore`
- `dotnet build`
- `dotnet test`

若某项任务无法完成，需清晰说明原因。  
若出现并发构建导致的临时包竞争或锁冲突，应顺序重试并报告最终结果。

---

## Project Structure Guidance (项目结构指引)
严格遵循 `project-structure.md` 文档要求。  
严格遵循 `implementation-rules.md` 文档要求。

补充结构原则：
- 领域真相放在 `FusionDomain`
- 调度协作边界放在 `FusionScheduler`
- 自动化投影边界放在 `FusionFA`
- 模块能力抽象放在 `FusionEquipment.Abstractions`
- UI 只放在 `FusionUI`
- 不得把所有类型堆放在项目根目录
- 目录结构与命名空间需一致

---

## Output Expectations (输出要求)
每个任务完成后，需报告：
- 创建的文件
- 修改的文件
- 新增/变更的项目引用
- 构建/测试状态
- 对现有模块边界是否有影响
- 建议的下一步操作

如果当前任务涉及以下内容，也必须额外说明：
- 是否复用了 FusionDomain 现有语义
- 是否新增了事件、命令、查询、视图或映射类型
- 是否对多进程演进友好
- 是否引入了新的跨模块依赖

---

## Review Red Flags (高风险信号)
以下情况默认应视为高风险并避免：
- 在 UI 中写业务规则
- 在 FA 中写调度逻辑
- 在 Scheduler 中写硬件驱动逻辑
- 在 Domain 中写协议或基础设施逻辑
- 直接共享可变运行态对象作为模块间真相
- 把 DomainEvent 当作 IPC 消息直接使用
- 为图方便增加跨层项目引用
- 用字符串硬编码领域状态而不是复用 Domain 枚举或值对象
