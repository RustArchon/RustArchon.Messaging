// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published on a fixed interval (see <c>ServerConnectionActor.ServerInfoPollInterval</c>) with the
/// handful of <c>serverinfo</c> fields worth graphing over time - player count, network throughput,
/// memory, and framerate. Deliberately narrower than the full <c>serverinfo</c> payload: the rest of
/// that payload (hostname, map, version, uptime, ...) is point-in-time server metadata with nothing to
/// trend, so the Panel fetches it live, on demand, via the existing generic
/// <see cref="SendRconCommand"/> pathway instead of persisting it here.
/// </summary>
public record ServerInfoSnapshotCaptured(
    Guid ServerId,
    Guid TenantId,
    int Players,
    int MaxPlayers,
    int NetworkIn,
    int NetworkOut,
    int Memory,
    decimal Framerate,
    DateTimeOffset CapturedAtUtc);
