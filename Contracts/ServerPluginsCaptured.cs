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
public record ServerPluginsCaptured(
    Guid ServerId,
    Guid TenantId,
    ServerModFramework Framework,
    IReadOnlyList<ServerPluginInfo> Plugins,
    DateTimeOffset CapturedAtUtc);
