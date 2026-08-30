// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published roughly every 10 seconds by a Worker's connection actor for as long as it - and the
/// worker process hosting it - is making forward progress, whether or not the underlying WebRCON
/// socket is currently connected.
/// </summary>
/// <remarks>
/// This answers "is a worker still responsible for this server at all," which is deliberately a
/// different question from <see cref="ConnectionStatusChanged"/>'s "is the socket up right now" - an
/// actor legitimately retrying a genuinely offline game server should keep heartbeating throughout,
/// not be treated as needing reassignment. Consumed only by the API (updates
/// <c>RustServer.AssignedWorkerId</c>/<c>LastHeartbeatUtc</c>); not relayed to end users.
/// </remarks>
public record ServerConnectionHeartbeat(Guid ServerId, Guid WorkerId, DateTimeOffset AtUtc);
