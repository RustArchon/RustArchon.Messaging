// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published for every currently-connected player on each <c>playerlist</c> reconciliation poll (not
/// just newly-connected ones) - carries the live stats that poll snapshot has and a fresh
/// <see cref="PlayerConnected"/>/text-detected join never does (a player's ping and Rust's own
/// anti-cheat violation level are only ever visible via <c>playerlist</c>, not the console join line).
/// </summary>
/// <remarks>
/// Distinct from <see cref="PlayerConnected"/>/<see cref="PlayerDisconnected"/>, which only fire on an
/// actual state transition - this fires every poll cycle for everyone already known to be online, so
/// their session's "last known ping/violation level" stays current for as long as they're connected
/// (and simply stops updating, rather than being cleared, once they disconnect).
/// </remarks>
public record PlayerSessionSnapshotUpdated(
    Guid ServerId,
    Guid TenantId,
    string SteamId,
    int Ping,
    decimal ViolationLevel,
    DateTimeOffset ObservedAtUtc);
