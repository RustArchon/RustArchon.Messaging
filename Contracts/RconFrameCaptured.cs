// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published for every WebRCON frame a connection actor sends or receives, whether it's unsolicited
/// console output, chat, a kill-feed line, a command's response, or the command itself.
/// </summary>
/// <remarks>
/// <para>
/// A command's response is not treated as a special case - it is captured and published exactly the
/// same way as any other frame, and separately resolves a pending command correlation inside the
/// actor that sent it. This is the raw capture path behind every persisted <c>RconEvent</c> and every
/// live-tailed console line.
/// </para>
/// <para>
/// <strong>Everything is captured; nothing is suppressed here.</strong> This type used to be published
/// selectively - a command sent with <c>RconCommandContext.Background</c> never generated one at all,
/// which was this codebase's second attempt at fixing the recurring "a backend-initiated command
/// showed up in the Panel's Console tab" bug (see <see cref="RconCommandContext"/>'s remarks for the
/// full incident). That approach traded one bug for a worse one: a site admin troubleshooting a
/// customer's server could no longer see the backend's own polling activity at all, even when they
/// legitimately needed to (e.g. confirming <c>playerlist</c>/<c>serverinfo</c> polling is actually
/// reaching the server). <see cref="Interactive"/> now rides along as data instead of gating whether
/// this publishes - every captured frame is persisted, and filtering who gets to <em>see</em> a
/// non-interactive one happens entirely server-side, at read time (<c>RustServersController.GetEvents</c>)
/// and at live-delivery time (<c>RconHub</c>'s group split) - never by simply not capturing it.
/// </para>
/// <para>
/// <see cref="Type"/> is captured verbatim as a string - the one public documented example of the
/// wire format shows it as a bare JSON number, but its exact meaning and shape aren't documented
/// anywhere reliable (RustArchon.Rcon's <c>WebRconResponse.Type</c>, where this value comes from,
/// tolerates either a JSON string or number for exactly this reason). No enum is defined for it here.
/// Classifying frames (chat vs. kill-feed vs. generic console spam) is explicitly deferred to a later
/// reader-side layer. A <see cref="RconEventDirection.Sent"/> frame has no meaningful <see cref="Type"/>
/// (there's no response yet) - it's published as an empty string.
/// </para>
/// </remarks>
/// <param name="Interactive">
/// Whether a human actually triggered this - carried through from <c>RconCommandContext.Interactive</c>
/// for a command's Sent/Received pair, or <c>true</c> for unsolicited output (chat, kill-feed, console
/// spam), since none of that is ever backend-initiated. Persisted on every <c>RconEvent</c> row
/// regardless of value; it's what every read/delivery boundary filters on to decide who may see a
/// <c>false</c> row.
/// </param>
/// <param name="Direction">Whether this is the command RustArchon sent, or something received back.</param>
public record RconFrameCaptured(
    Guid ServerId,
    Guid TenantId,
    DateTimeOffset CapturedAtUtc,
    int Identifier,
    string Type,
    string Message,
    string? Stacktrace,
    bool Interactive,
    RconEventDirection Direction);
