// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Worker each time it reads a server's RustArchon plugin for the state of the world map
/// (<c>archon.map.status</c>): which world it is, whether the game has drawn a picture of it, and how an upload is going.
/// The Api records it and decides whether the picture needs collecting.
/// </summary>
/// <param name="WorldSize">The world's size in metres (it is square).</param>
/// <param name="WorldSeed">The world's seed; with the size it identifies a wipe.</param>
/// <param name="FileName">The picture file's name on the game server, <c>map_{size}_{seed}.png</c>.</param>
/// <param name="Exists">Whether that file exists on the game server.</param>
/// <param name="Bytes">Its size in bytes (0 when it does not exist).</param>
/// <param name="UploadState">The plugin's own upload state: <c>idle</c> (never asked), <c>uploading</c>, <c>done</c> or
/// <c>failed</c>.</param>
/// <param name="MonumentsJson">The named places on the map, the plugin's <c>monuments</c> array exactly as it sent it.
/// Sent only the first time the Worker sees a world (they do not change until the next wipe); otherwise <c>null</c>.</param>
public record PluginMapStatusCaptured(
    Guid ServerId,
    Guid TenantId,
    int WorldSize,
    long WorldSeed,
    string FileName,
    bool Exists,
    long Bytes,
    string UploadState,
    string? MonumentsJson,
    DateTimeOffset CapturedAtUtc);
