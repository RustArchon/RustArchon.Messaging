// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>Which mod framework a server runs, as far as its <c>o.version</c>/<c>c.version</c> responses say.</summary>
public enum ServerModFramework
{
    /// <summary>Neither Oxide nor Carbon answered - a vanilla server (or one whose framework isn't recognized).</summary>
    None = 0,
    Oxide = 1,
    Carbon = 2
}

/// <summary>One loaded plugin, as reported by <c>o.plugins</c> / <c>c.plugins</c>.</summary>
public record ServerPluginInfo(string Name, string Author, string Version);

/// <summary>
/// One reason a plugin file on a server did not load, as Carbon lists it under "failed plugins" in <c>c.plugins</c>: the file, the place in it
/// and what the compiler said. A file that failed for several reasons has one of these for each.
/// </summary>
/// <param name="File">The plugin's file name as Carbon shows it, for example <c>BotReSpawn.cs</c>.</param>
/// <param name="Line">The line the compiler pointed at.</param>
/// <param name="Column">The column the compiler pointed at.</param>
/// <param name="Message">The compiler's message, with the pieces Carbon wrapped onto further lines joined back together.</param>
public record ServerPluginFailure(string File, int Line, int Column, string Message);

/// <summary>
/// Published periodically (see <c>ServerConnectionActor.PluginListPollInterval</c>) with the complete
/// list of plugins currently loaded on a server - the whole list every time, not a delta, so the
/// consumer can make the stored rows match it exactly (an unloaded plugin disappears, an upgraded one
/// changes version) without tracking anything between messages.
/// </summary>
/// <remarks>
/// <see cref="Plugins"/> empty with <see cref="Framework"/> <see cref="ServerModFramework.None"/> is a
/// real, meaningful message - "this server has no plugin framework" - and is what clears rows left over
/// from a framework that has since been uninstalled.
/// </remarks>
/// <param name="Failures">
/// The plugin files Carbon says failed to load, from the same reply. <c>null</c> means "not reported" (an older Worker, Oxide, or a reply without
/// the section) and leaves what is stored alone; an empty list means "none failed" and clears it. A list is the whole current set every time,
/// like <see cref="Plugins"/>.
/// </param>
public record ServerPluginsCaptured(
    Guid ServerId,
    Guid TenantId,
    ServerModFramework Framework,
    IReadOnlyList<ServerPluginInfo> Plugins,
    DateTimeOffset CapturedAtUtc,
    IReadOnlyList<ServerPluginFailure>? Failures = null);
