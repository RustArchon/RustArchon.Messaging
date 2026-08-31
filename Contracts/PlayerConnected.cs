// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published the moment a Worker sees a player join, either from parsing the unsolicited
/// "&lt;ip&gt;:&lt;port&gt;/&lt;steamId&gt;/&lt;name&gt; joined [...]" console line (the normal,
/// real-time path - see <c>PlayerConnectionTextParser</c> in RustArchon.Rcon) or, for someone already
/// connected when the Worker's actor starts (no console line exists for that case), from its periodic
/// <c>playerlist</c> poll reconciling against what it already knows (see <c>ServerConnectionActor</c>).
/// </summary>
/// <remarks>
/// Rust's WebRCON has no distinct frame <c>Type</c> for this - it's ordinary <c>Generic</c> console
/// text like everything else - but confirmed live against a real production server, the line itself
/// is reliable and arrives immediately, so this is genuinely real-time in the common case, not
/// poll-interval-bounded the way it originally was.
/// </remarks>
public record PlayerConnected(
    Guid ServerId,
    Guid TenantId,
    string SteamId,
    string DisplayName,
    string IpAddress,
    DateTimeOffset ConnectedAtUtc);
