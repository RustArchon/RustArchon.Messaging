// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// The kind of change described by a <see cref="ServerLifecycleChanged"/> message.
/// </summary>
/// <remarks>
/// Deliberately excludes creation and enable/disable - those are ownership transitions (see
/// <see cref="ConnectToServer"/>), not refreshes of an existing connection.
/// </remarks>
public enum ServerLifecycleChangeType
{
    /// <summary>The server's connection details (host, port, or RCON password) changed.</summary>
    Updated,

    /// <summary>The server was disabled or deleted and any existing connection should stop.</summary>
    Disabled,

    /// <summary>The server was deleted outright.</summary>
    Deleted
}

/// <summary>
/// Published whenever a server's connection details change or it stops needing a connection.
/// </summary>
/// <remarks>
/// Delivered as a <b>fanout</b> message: every live Worker instance receives its own copy. Each
/// instance self-filters by whether it currently holds a connection actor for <see cref="ServerId"/>
/// - the instance that does reacts (<see cref="ServerLifecycleChangeType.Updated"/> refreshes the
/// connection with new credentials; <see cref="ServerLifecycleChangeType.Disabled"/>/
/// <see cref="ServerLifecycleChangeType.Deleted"/> stop and remove it), every other instance no-ops.
/// No <c>WorkerId</c> targeting is needed since "do I have an actor for this id" is already the
/// correct filter.
/// </remarks>
public record ServerLifecycleChanged(Guid ServerId, Guid TenantId, ServerLifecycleChangeType ChangeType);
