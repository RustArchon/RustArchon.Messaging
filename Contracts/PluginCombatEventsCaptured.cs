// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Worker each time it drains a batch of combat events from a server's RustArchon plugin
/// (<c>archon.events.drain</c>), for the Api to store.
/// </summary>
/// <param name="BootId">Identifies one run of the plugin: its sequence numbers restart at 1 whenever it reloads, so
/// a sequence number only means something together with the boot it came from.</param>
/// <param name="Reset">The plugin reloaded since the Worker last asked, so this batch starts a new boot.</param>
/// <param name="Lost">The plugin's buffer wrapped before the Worker collected everything: there is a gap before
/// <paramref name="FirstSequence"/>. The Panel shows the gap rather than inventing what was in it.</param>
/// <param name="EventsJson">The plugin's events exactly as it sent them: a JSON array in the plugin's compact format
/// (format 1). Stored as is, so a change to how events are shown never needs a re-drain.</param>
public record PluginCombatEventsCaptured(
    Guid ServerId,
    Guid TenantId,
    long BootId,
    bool Reset,
    bool Lost,
    long FirstSequence,
    long LastSequence,
    int EventCount,
    string EventsJson,
    DateTimeOffset CapturedAtUtc);
