// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// One plugin that UpdateChecker (a third-party plugin on the game server) says has a newer version, exactly as it was reported:
/// nothing is derived or dropped, so the Panel can show all of it.
/// </summary>
/// <param name="Name">The plugin's name.</param>
/// <param name="CurrentVersion">The version UpdateChecker saw installed when it last said this.</param>
/// <param name="LatestVersion">The newest version it knows of.</param>
/// <param name="Url">The plugin's page on its marketplace. A page to visit, not a file to download; it came from a third party and is only ever offered as a link after the Api has checked it.</param>
/// <param name="Marketplace">Where the plugin is sold or hosted (uMod, Codefling and so on).</param>
/// <param name="FirstSeenUtc">When the RustArchon plugin first heard of this newest version.</param>
/// <param name="LastSeenUtc">When it last heard it (UpdateChecker repeats itself at every scan).</param>
/// <param name="TimesSeen">How many times it has heard it since the RustArchon plugin loaded.</param>
public record PluginUpdateNoticeInfo(
    string Name,
    string CurrentVersion,
    string LatestVersion,
    string Url,
    string Marketplace,
    DateTimeOffset FirstSeenUtc,
    DateTimeOffset LastSeenUtc,
    int TimesSeen);

/// <summary>
/// The update notices the RustArchon plugin holds for a server, read by the Worker (<c>archon.updates</c>). Published only when the
/// list has something in it and differs from the last one sent. It is a table of the newest notice per plugin, not an event
/// history, so the Api merges it into what it holds by plugin name.
/// </summary>
public record PluginUpdatesCaptured(
    Guid ServerId,
    Guid TenantId,
    IReadOnlyList<PluginUpdateNoticeInfo> Updates,
    DateTimeOffset CapturedAtUtc);
