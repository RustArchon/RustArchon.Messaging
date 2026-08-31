// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published when a Worker's kill-feed text parser (see <c>RustArchon.Rcon.KillFeedTextParser</c>)
/// matches an unsolicited console line as a death event - a player or NPC killing a player or NPC.
/// </summary>
/// <remarks>
/// <para>
/// <strong>This is heuristic, not authoritative.</strong> Rust's WebRCON has no structured "kill"
/// message - death events only ever show up as free-text console lines, and their exact wording
/// varies by game version, installed mods, and the specific circumstances of the death (explicit
/// player-vs-player, NPC-vs-player, environmental, suicide, and various plugin-injected formats all
/// look different). <see cref="RawMessage"/> is always the untouched source line specifically so a
/// misparse is auditable rather than silently trusted. The reliable long-term fix is a companion
/// Rust plugin reporting deaths via its own hook (<c>OnEntityDeath</c>/<c>OnPlayerDeath</c> in
/// Oxide/Carbon) rather than scraping text - this event's shape doesn't need to change when that
/// exists, only what publishes it.
/// </para>
/// <para>
/// <see cref="VictimSteamId"/>/<see cref="KillerSteamId"/> are <c>null</c> when that party is an NPC
/// (a scientist, animal, or similar) rather than a player - <see cref="VictimName"/>/
/// <see cref="KillerName"/> are always populated regardless. <see cref="KillerName"/>/
/// <see cref="KillerSteamId"/> are both <c>null</c> when the line doesn't identify a killer at all
/// (e.g. "died (Generic)" - environmental/unspecified death).
/// </para>
/// </remarks>
public record PlayerKilled(
    Guid ServerId,
    Guid TenantId,
    DateTimeOffset OccurredAtUtc,
    string VictimName,
    string? VictimSteamId,
    string? KillerName,
    string? KillerSteamId,
    string? Weapon,
    string RawMessage);
