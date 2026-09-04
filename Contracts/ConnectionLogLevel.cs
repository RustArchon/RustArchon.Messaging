// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Severity of one <see cref="WorkerDiagnosticLogged"/> entry (or, for a <see cref="ConnectionStatusChanged"/>
/// transition once it's persisted, the severity derived from its <see cref="RconConnectionStatus"/> -
/// see <c>ConnectionStatusConsumer</c>'s remarks). Deliberately just three values, not one per
/// exception type or subsystem - this drives nothing but which color badge the Panel's Logs tab shows.
/// </summary>
public enum ConnectionLogLevel
{
    Info,
    Warning,
    Error
}
