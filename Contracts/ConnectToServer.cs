// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published whenever some live Worker instance should own the persistent WebRCON connection for a
/// server - on creation, on being re-enabled, and periodically by the API's claim-sweep for any
/// server whose heartbeat has gone stale. Delivered as a <b>competing consumer</b> message: exactly
/// one live Worker instance receives each publish (RabbitMQ's default fair dispatch across every
/// instance sharing the consumer's queue) - this is what lets any number of Worker instances share
/// the total set of servers without coordinating with each other directly.
/// </summary>
public record ConnectToServer(Guid ServerId, Guid TenantId);
