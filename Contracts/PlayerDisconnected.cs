// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published the moment a Worker sees a player leave - normally parsed in real time from the
/// unsolicited "&lt;ip&gt;:&lt;port&gt;/&lt;steamId&gt;/&lt;name&gt; disconnecting: &lt;reason&gt;"
/// console line (see <c>PlayerConnectionTextParser</c> in RustArchon.Rcon), with the periodic
/// <c>playerlist</c> poll in <c>ServerConnectionActor</c> only as a fallback for a missed/garbled
/// line. See <see cref="PlayerConnected"/>'s remarks for the equivalent join-side detail.
/// </summary>
public record PlayerDisconnected(
    Guid ServerId,
    Guid TenantId,
    string SteamId,
    DateTimeOffset DisconnectedAtUtc);
