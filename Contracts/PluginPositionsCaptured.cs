// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Worker each time it drains a batch of player position samples from a server's RustArchon plugin
/// (<c>archon.positions.drain</c>), for the Api to store.
/// </summary>
/// <param name="BootId">Identifies one run of the plugin: its sequence numbers restart at 1 whenever it reloads, so
/// a sequence number only means something together with the boot it came from.</param>
/// <param name="Reset">The plugin reloaded since the Worker last asked, so this batch starts a new boot.</param>
/// <param name="Lost">The plugin's buffer wrapped before the Worker collected everything: there is a gap before
/// <paramref name="FirstSequence"/>. A replay shows the gap rather than inventing where the player went.</param>
/// <param name="SamplesJson">The plugin's samples exactly as it sent them: a JSON array in the plugin's compact format
/// (format 1). Stored as is, so a change to how they are shown never needs a re-drain.</param>
public record PluginPositionsCaptured(
    Guid ServerId,
    Guid TenantId,
    long BootId,
    bool Reset,
    bool Lost,
    long FirstSequence,
    long LastSequence,
    int SampleCount,
    string SamplesJson,
    DateTimeOffset CapturedAtUtc);
