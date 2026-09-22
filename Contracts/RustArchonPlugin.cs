// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// What the optional RustArchon companion plugin is called and which capabilities it can report, in one place
/// so the Worker, Api and Panel cannot drift apart on a string literal. See
/// <c>docs/plans/companion-server-plugin.md</c>.
/// </summary>
/// <remarks>
/// A capability appears in a handshake only once the installed plugin build can actually perform it, and a
/// Panel feature is offered only when its capability is present (fail closed).
/// </remarks>
public static class RustArchonPlugin
{
    /// <summary>The plugin's name as it appears in <c>o.plugins</c> / <c>c.plugins</c>.</summary>
    public const string Name = "RustArchon";

    /// <summary>The plugin accepts <c>archon.config</c>, i.e. its switches can be changed from the Panel.</summary>
    public const string ConfigCapability = "config";

    /// <summary>The Updater plugin's name as it appears in <c>o.plugins</c> / <c>c.plugins</c>: a separate, tiny plugin that swaps the main one and rolls back on failure.</summary>
    public const string UpdaterName = "RustArchonUpdater";

    /// <summary>The plugin can record positions and events (the Recording switch does something).</summary>
    public const string RecordingCapability = "recording";

    /// <summary>The plugin can track combat (the Combat log switch does something).</summary>
    public const string CombatCapability = "combat";

    /// <summary>The plugin keeps an index of players' tool cupboards (who owns them, where they are, who is authorized), read with <c>archon.tcs</c>.</summary>
    public const string TcsCapability = "tcs";

    /// <summary>The plugin records where connected players are (while Recording is on), drained with <c>archon.positions.drain</c>.</summary>
    public const string PositionsCapability = "positions";

    /// <summary>The plugin can draw and report the world map (<c>archon.map.status</c>, <c>.render</c>, <c>.monuments</c>, <c>.upload</c>).</summary>
    public const string MapCapability = "map";

    /// <summary>The plugin keeps the update notices UpdateChecker reports and hands them over (<c>archon.updates</c>).</summary>
    public const string UpdatesCapability = "updates";

    /// <summary>
    /// The plugin can install, replace and roll back the Updater plugin itself (<c>archon.updater.update</c>). The Updater can never
    /// replace itself, since a failed one has nothing left to recover it; the main plugin is its way back, and the Updater is the main
    /// plugin's. Without this capability the Updater still has to be installed by hand.
    /// </summary>
    public const string UpdaterUpdateCapability = "updater-update";

    /// <summary>
    /// The plugin can apply a newer version of another plugin that is already installed (<c>archon.thirdparty.update</c> and <c>.status</c>): it
    /// downloads its own copy, applies it only if it is the file the Panel checked, keeps the old one as a backup and puts it back if the new one does
    /// not load. Without this capability nothing is applied to third-party plugins on that server.
    /// </summary>
    public const string ThirdPartyUpdateCapability = "thirdparty-update";

    /// <summary>
    /// The plugin can also apply an update that is a zip archive, following a person's folder rules (<c>archon.thirdparty.zip</c>). Listed only when the
    /// server's own runtime can actually read zip files, which the plugin checks when it starts: the reader is loaded by name at run time, so a server
    /// that lacks it costs nothing but this capability. Without it a zip update is never sent to that server.
    /// </summary>
    public const string ThirdPartyZipCapability = "thirdparty-zip";

    /// <summary>
    /// The header a game server's map upload carries its one-time token in. In a header rather than the address so the token is
    /// not recorded wherever addresses are (access logs, proxies, history); the address names only the server.
    /// </summary>
    public const string MapUploadTokenHeader = "X-RustArchon-Upload-Token";

    /// <summary>
    /// The header the Updater's plugin download carries its one-time token in (from Updater 0.3.0). Same reasoning as
    /// <see cref="MapUploadTokenHeader"/>: not in the address, so no access log or proxy keeps it. Updaters before 0.3.0 put the token
    /// in the address, and that form is still served.
    /// </summary>
    public const string UpdateTokenHeader = "X-RustArchon-Update-Token";

    /// <summary>
    /// Whether a list of plugin names positively shows the RustArchon plugin loaded. Exact name, ignoring case:
    /// a lookalike such as "RustArchonHelper" does not count.
    /// </summary>
    public static bool IsListed(IEnumerable<string> pluginNames) =>
        pluginNames.Any(n => string.Equals(n, Name, StringComparison.OrdinalIgnoreCase));
}

/// <summary>
/// The states the plugin's check of its own file against the Panel's signature can report - see the plugin's
/// <c>ArchonIntegrity</c>. Values arrive from a plugin running on someone else's server, so anything outside this
/// set is normalized to <see cref="Unknown"/> rather than stored: never trust a free-form string from that side.
/// </summary>
public static class PluginSigningStates
{
    /// <summary>The file carries a signature that verifies under the key stamped into it.</summary>
    public const string Valid = "valid";

    /// <summary>A signature is present and does not verify: the file was altered, re-encoded, or signed by another key.</summary>
    public const string Invalid = "invalid";

    /// <summary>No key stamped in, or no signature line: a developer copy, not one downloaded from a Panel.</summary>
    public const string Unsigned = "unsigned";

    /// <summary>The plugin could not find its own file to read.</summary>
    public const string Unlocated = "unlocated";

    /// <summary>The runtime could not perform the check.</summary>
    public const string Error = "error";

    /// <summary>Not reported (a plugin build from before signing existed), or not a recognized value.</summary>
    public const string Unknown = "unknown";

    private static readonly HashSet<string> Known = [Valid, Invalid, Unsigned, Unlocated, Error, Unknown];

    /// <summary>The state as one of the known values; anything else (including null) becomes <see cref="Unknown"/>.</summary>
    public static string Normalize(string? state)
    {
        var lowered = state?.Trim().ToLowerInvariant();
        return lowered is not null && Known.Contains(lowered) ? lowered : Unknown;
    }

    /// <summary>
    /// A key fingerprint as the plugin reports it (16 lowercase hex characters), or empty if it is anything else.
    /// Bounded and shape-checked because it comes from a plugin on someone else's server and is stored and shown.
    /// </summary>
    public static string NormalizeFingerprint(string? fingerprint)
    {
        var trimmed = fingerprint?.Trim().ToLowerInvariant() ?? "";
        return trimmed.Length == 16 && trimmed.All(c => c is (>= '0' and <= '9') or (>= 'a' and <= 'f')) ? trimmed : "";
    }
}
