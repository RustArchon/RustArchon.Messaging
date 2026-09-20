// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Worker when the optional RustArchon companion plugin answered its <c>archon.hello</c>
/// handshake - what the plugin is, which protocol it speaks, what it can do, and the state of its two
/// switches. See <c>docs/plans/companion-server-plugin.md</c>.
/// </summary>
/// <remarks>
/// <para>
/// Only ever published for a server whose plugin list <em>positively</em> shows the plugin loaded (the
/// Worker never probes a server that doesn't list it), and only when the plugin's reply parsed as a valid
/// envelope. Its absence is therefore not itself a message: whether the plugin is installed is read from the
/// stored plugin list, and features stay off unless a capability appears in <see cref="Capabilities"/>
/// (fail closed).
/// </para>
/// <para>
/// <see cref="RecordingEnabled"/> and <see cref="CombatLogEnabled"/> are what the plugin <em>reports</em>. The
/// Api compares them with the server's saved (desired) settings and pushes an <c>archon.config set</c>
/// command through the Worker when they differ, so a Panel toggle, a plugin reinstall and a reboot all
/// converge. <see cref="SettingsPersisted"/> is false when the plugin could not write its settings file,
/// meaning they revert on the next reload and the Api will simply re-apply them.
/// </para>
/// <para>
/// <see cref="SigningState"/> is the plugin's own check of its file against the signature a Panel put on it:
/// <c>valid</c>, <c>invalid</c>, <c>unsigned</c>, <c>unlocated</c> or <c>error</c>, or <c>unknown</c> for a plugin
/// build old enough not to report it. <see cref="SigningKeyFingerprint"/> names the key that plugin trusts (empty
/// when it has none). The last two are defaulted so a message published before they existed, still sitting in a
/// queue during an upgrade, is read as "unknown" rather than failing.
/// </para>
/// </remarks>
public record ServerPluginHandshakeCaptured(
    Guid ServerId,
    Guid TenantId,
    int ProtocolVersion,
    string PluginVersion,
    IReadOnlyList<string> Capabilities,
    bool RecordingEnabled,
    bool CombatLogEnabled,
    bool SettingsPersisted,
    DateTimeOffset CapturedAtUtc,
    string SigningState = "unknown",
    string SigningKeyFingerprint = "");
