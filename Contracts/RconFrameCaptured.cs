// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published for every WebRCON frame a connection actor receives, whether it's unsolicited console
/// output, chat, a kill-feed line, or the response to a command that was sent.
/// </summary>
/// <remarks>
/// <para>
/// A command's response is not treated as a special case - it is captured and published exactly the
/// same way as any other frame, and separately resolves a pending command correlation inside the
/// actor that sent it. This is the raw capture path behind every persisted <c>RconEvent</c> and every
/// live-tailed console line.
/// </para>
/// <para>
/// <see cref="Type"/> is captured verbatim as a string - the one public documented example of the
/// wire format shows it as a bare JSON number, but its exact meaning and shape aren't documented
/// anywhere reliable (RustArchon.Rcon's <c>WebRconResponse.Type</c>, where this value comes from,
/// tolerates either a JSON string or number for exactly this reason). No enum is defined for it here.
/// Classifying frames (chat vs. kill-feed vs. generic console spam) is explicitly deferred to a later
/// reader-side layer.
/// </para>
/// </remarks>
public record RconFrameCaptured(
    Guid ServerId,
    Guid TenantId,
    DateTimeOffset CapturedAtUtc,
    int Identifier,
    string Type,
    string Message,
    string? Stacktrace);
