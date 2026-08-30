// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Request to run a command on a server's live RCON connection.
/// </summary>
/// <remarks>
/// Delivered as a <b>fanout</b> message, like <see cref="ServerLifecycleChanged"/>: every live Worker
/// instance receives its own copy and checks whether it currently holds the connection actor for
/// <see cref="ServerId"/>. Only the instance that does calls a response back; every other instance
/// consumes the request and does nothing, so the request client's one real response always comes from
/// the true owner - this is the entire mechanism by which command dispatch works correctly at any
/// worker count without either side needing to know ownership ahead of time.
/// </remarks>
public record SendRconCommand(Guid ServerId, string Command);

/// <summary>
/// Response to a <see cref="SendRconCommand"/> request.
/// </summary>
/// <remarks>
/// <see cref="Error"/> is populated only when <see cref="Success"/> is <c>false</c> - e.g.
/// <c>"NotConnected"</c> when the owning instance's socket isn't up at this instant.
/// </remarks>
public record RconCommandResult(bool Success, string? Message, string? Type, string? Stacktrace, string? Error);
