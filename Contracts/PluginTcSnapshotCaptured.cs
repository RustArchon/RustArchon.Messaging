// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Worker each time it has read a server's tool cupboard index from the RustArchon plugin
/// (<c>archon.tcs</c>), for the Api to store as that server's current picture of its bases. Each one replaces the last:
/// it is a snapshot, not an event.
/// </summary>
/// <param name="Ready">The plugin's initial scan of the world had finished, so the list is complete. False means the
/// list may be missing cupboards, and the Panel says so.</param>
/// <param name="Count">How many cupboards <paramref name="TcsJson"/> holds.</param>
/// <param name="TcsJson">The cupboards as the plugin sent them: a JSON array in its compact format (format 1).</param>
public record PluginTcSnapshotCaptured(
    Guid ServerId,
    Guid TenantId,
    bool Ready,
    int Count,
    string TcsJson,
    DateTimeOffset CapturedAtUtc);
