namespace FusionLog.Entries;

/// <summary>
/// 表示日志消息的最小模型。
/// </summary>
/// <param name="Text">消息文本。</param>
public sealed record LogMessage(string Text);
