// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published for a worker-side event worth surfacing in the Panel's Logs tab that is <em>not</em>
/// itself a connection-status transition - a frame/parse error, a poll failure, and the like.
/// </summary>
/// <remarks>
/// Kept as a separate message type from <see cref="ConnectionStatusChanged"/> rather than folded into
/// it (e.g. as another <see cref="RconConnectionStatus"/> value) on purpose: a parse error or a poll
/// failure doesn't mean the connection itself is down - the socket can be perfectly healthy while one
/// poll's JSON fails to deserialize - so publishing it through the status pipeline would incorrectly
/// flip the live "is this server connected" badge (<c>RustServer.ConnectionStatus</c>, the header pill,
/// <c>ServersList.razor</c>'s icons) for something that isn't actually a connection problem. This
/// message only ever appends to the connection log; it never touches connection state.
/// </remarks>
public record WorkerDiagnosticLogged(
    Guid ServerId,
    Guid TenantId,
    ConnectionLogLevel Level,
    string Message,
    DateTimeOffset OccurredAtUtc);
