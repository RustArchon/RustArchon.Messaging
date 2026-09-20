// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published by the Api when a server's desired RustArchon-plugin switches (Recording, Combat log) have just
/// been changed from the Panel, so the plugin can be brought in line immediately instead of at the next
/// handshake. Carries no values: the consumer reads the server's saved settings, so a message that arrives
/// late or twice can only ever apply the current desired state.
/// </summary>
public record ServerPluginSettingsChanged(Guid ServerId, Guid TenantId);
