// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>The polls <see cref="PollServerNow"/> can ask for.</summary>
public static class PollServerNowKinds
{
    /// <summary>The installed plugin list (<c>c.plugins</c>/<c>o.plugins</c>) and, when the RustArchon plugin is listed, its <c>archon.hello</c>
    /// handshake - the same pair the background poll always runs together (see <c>ServerConnectionActor.PluginListPollLoopAsync</c>).</summary>
    public const string Plugins = "plugins";

    /// <summary>The plugin's UpdateChecker notices (<c>archon.updates</c>), while it reports the capability.</summary>
    public const string Updates = "updates";
}

/// <summary>
/// Asks the Worker instance that owns this server's connection to run one or more polls right now, instead of waiting for their usual few-minute
/// schedule. Used when a person presses Refresh on the Plugins tab, and right after the Api confirms a plugin update took effect, so what is shown
/// reflects the server within seconds rather than however long is left on the background timer.
/// </summary>
/// <remarks>
/// <para>
/// Delivered as a <b>fanout</b> message, exactly like <see cref="SendRconCommand"/>: every live Worker instance receives its own copy and checks
/// whether it currently holds the connection actor for <see cref="ServerId"/>. Only the instance that does responds; every other instance consumes
/// the request and does nothing, so the request client's one real response always comes from the true owner.
/// </para>
/// <para>
/// This runs the poll inline, once, on top of - not instead of - the connection actor's own background schedule; it does not reset that schedule's
/// timer. A poll shortly before this one would have fired anyway is a harmless redundant round trip, not a bug: the alternative (synchronizing this
/// with the background loop's own delay) is not worth the complexity for something rate-limited to occasional use.
/// </para>
/// </remarks>
public record PollServerNow(Guid ServerId, IReadOnlyList<string> Polls);

/// <summary>
/// Response to a <see cref="PollServerNow"/> request. <see cref="Connected"/> is <c>false</c> when the server's connection was not up at the moment
/// this ran, in which case nothing was asked of it and nothing changed - the caller is looking at whatever was already stored.
/// </summary>
public record PollServerNowResult(bool Connected);
