// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// The state of a connection actor's underlying WebRCON socket.
/// </summary>
public enum RconConnectionStatus
{
    Disconnected,
    Connecting,
    Connected,
    Reconnecting,
    Error
}

/// <summary>
/// Published by a connection actor every time its underlying WebRCON socket's state changes.
/// </summary>
/// <remarks>
/// This is the UI-facing "is the socket actually up right now" signal, distinct from
/// <see cref="ServerConnectionHeartbeat"/>'s "is a worker still responsible for this server at all" -
/// a server can have a very fresh heartbeat while its status is <see cref="RconConnectionStatus.Reconnecting"/>.
/// </remarks>
public record ConnectionStatusChanged(
    Guid ServerId,
    Guid TenantId,
    RconConnectionStatus Status,
    string? Detail,
    DateTimeOffset ChangedAtUtc);
