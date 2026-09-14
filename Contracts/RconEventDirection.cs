// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Whether a captured WebRCON frame is the command RustArchon sent, or something received back over
/// the connection (a command's response, or unsolicited console/chat/kill-feed output).
/// </summary>
/// <remarks>
/// Added alongside <see cref="RconFrameCaptured.Interactive"/> as part of persisting every captured
/// frame rather than suppressing background ones at the source - see that record's remarks. Before
/// this, only responses were ever captured; a sent command was inferable, at best, from the next
/// response's <c>Message</c> echoing it back (not every command does). <c>Sent</c> is ordered before
/// <c>Received</c> to match the two frames' real chronological order for the same command, not because
/// the numeric value is stored anywhere order-sensitive.
/// </remarks>
public enum RconEventDirection
{
    /// <summary>A command RustArchon sent, captured before any response for it arrives (if one ever does).</summary>
    Sent,

    /// <summary>A frame received from the server - a command's response, or unsolicited output.</summary>
    Received
}
