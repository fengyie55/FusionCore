# AGENTS.md

## Project
FusionCore 是基于 WPF 和 .NET 构建的模块化半导体设备控制平台。
该项目采用架构驱动的开发模式，在实现前需遵循已批准的设计文档。

## Current Phase (当前阶段)
Phase 1: 代码仓库和解决方案骨架初始化。

当前目标：
- 创建清晰的解决方案结构
- 搭建基准项目
- 建立有效的项目引用关系
- 保证解决方案可编译构建
- 避免实现复杂的业务逻辑

## Required Reading Before Changes (变更前必读文档)
修改代码前必须阅读以下文件：
1. `implementation-rules.md`
2. `project-structure.md`
3. `docs/architecture/` 目录下的所有文档

优先级顺序：
- 架构文档
- 实现规则
- 项目结构文档
- 任务需求文档

## Hard Constraints (强制约束)
- 不得违反层边界规则。
- 不得在 FusionUI 中放置业务逻辑。
- 不得让 FusionDomain 依赖 WPF、数据库、网络或协议实现类库。
- 不得让 FusionScheduler 直接控制 IO、运动控制或协议驱动。
- 不得让 FusionFA 实现内部设备调度逻辑。
- 不得在 `FusionEquipment.Abstractions` 中放置 PM/TM/CM/BM 的具体实现。
- **所有代码注释（包括单行注释、多行注释、XML 文档注释）必须使用中文编写，禁止使用英文注释。**

## Phase-1 Restrictions (第一阶段限制)
在第一阶段，禁止：
- 实现完整的 SECS/GEM 协议行为
- 实现生产调度算法
- 引入数据库持久化耦合
- 添加不必要的第三方框架
- 为图方便创建跨层的快捷调用方式

## Working Style (工作规范)
每个任务执行流程：
1. 总结实现计划
2. 列出待创建/修改的文件
3. 做最小化、整洁的代码变更
4. 运行验证命令
5. 清晰报告结果

## Validation (验证流程)
代码变更后，按需运行以下命令：
- `dotnet restore`
- `dotnet build`
- `dotnet test`

若某项任务无法完成，需清晰说明原因。

## Project Structure Guidance (项目结构指引)
严格遵循 `project-structure.md` 文档要求。
严格遵循 `implementation-rules.md` 文档要求。

## Output Expectations (输出要求)
每个任务完成后，需报告：
- 创建的文件
- 修改的文件
- 新增/变更的项目引用
- 构建/测试状态
- 建议的下一步操作
