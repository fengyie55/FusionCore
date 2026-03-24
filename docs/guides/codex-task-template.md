# FusionCore Codex Task Template

## Purpose

This file provides a reusable task preamble template for Codex work in the FusionCore repository.

Use this template when:
- starting a new Codex implementation task
- constraining a phase-specific code generation task
- ensuring architecture, standards, and multi-process guardrails are consistently applied
- reducing prompt drift across repeated Codex tasks

This file is not a replacement for:
- `AGENTS.md`
- `implementation-rules.md`
- `project-structure.md`
- `docs/architecture/`
- `docs/standards/`
- `docs/guides/`

It is a reusable task-layer wrapper that should be copied and then customized for each concrete task.

---

## How to Use

For each new Codex task:

1. Copy one of the templates below
2. Fill in the task-specific placeholders
3. Keep the repository-level rules unchanged
4. Only change:
   - task name
   - task goal
   - extra restrictions
   - allowed modules
   - acceptance focus

Use the **full template** for architecture-sensitive or multi-file changes.
Use the **short template** for small scoped tasks.

---

## Full Task Template

```text
你正在为 FusionCore 项目执行一个受架构约束的实现任务。

项目名称：FusionCore

在开始前，请先阅读并严格遵守以下文件：
- AGENTS.md
- implementation-rules.md
- project-structure.md
- docs/architecture/ 下的全部正式设计文档
- docs/standards/ 下的全部 Markdown 文件
- docs/guides/ 下的全部 Markdown 文件

请特别注意以下原则：
1. 严格遵守分层架构与模块职责边界
2. 优先复用 FusionDomain 现有语义，不重复创造相同概念
3. 默认按多进程架构方向实现，不要假设所有模块交互都是同进程方法调用
4. DomainEvent 是领域事实，不自动等价于 IPC / Integration Message
5. 不得引入未在当前阶段批准的复杂协议、算法、数据库或基础设施实现
6. 所有代码注释（包括 XML 文档注释）必须使用中文
7. 优先做最小改动，不要无故重构现有骨架

本轮任务名称：
【在这里填写任务名称】

本轮任务目标：
【在这里填写本轮只允许实现的内容】

本轮必须遵守的额外限制：
【在这里填写本轮特别不能做的内容】

本轮涉及模块：
【在这里填写本轮允许修改的项目/模块】
例如：
- FusionDomain
- FusionScheduler
- FusionFA
- FusionEquipment.Abstractions
- FusionUI
- FusionKernel
- tests/ 对应测试项目

修改前请先输出：
1. 计划新增或修改的目录
2. 计划新增或修改的文件
3. 计划新增的核心类型 / 接口 / 事件 / 命令 / 查询 / 模型
4. 如需微调命名、目录结构或已有骨架，请先说明理由，再开始实施

实现要求：
- 保持代码简洁、清晰、可扩展
- 命名空间与目录结构保持一致
- 不要把所有类型堆放在项目根目录
- 不要新增不必要的第三方依赖
- 不要修改无关模块
- 如果确需补充已有模块中的极小类型，请明确说明原因
- 保持实现对多进程演进友好
- 契约与模型尽量保持可序列化、可代理、轻量化

验证要求：
完成后请执行：
- dotnet restore
- dotnet build
- dotnet test

如果过程中出现并发导致的包锁、构建锁或临时失败，请按顺序重试，并报告最终结果。

最终汇报必须包含：
1. 新增了哪些目录和文件
2. 修改了哪些目录和文件
3. 新增了哪些核心类型
4. 是否复用了 FusionDomain 现有语义
5. 是否新增或修改了项目引用关系
6. restore / build / test 的结果
7. 对现有模块职责边界是否有影响
8. 当前仍缺失但应留到下一步的内容
9. 建议的下一步任务

如果发现当前任务目标与架构文档冲突：
- 不要擅自改变架构
- 请先指出冲突点
- 给出最小影响的处理建议
- 等待进一步确认后再继续
