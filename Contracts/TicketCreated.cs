// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// "A ticket was submitted." Published by <c>RustArchon.Api</c>'s <c>TicketsController</c> and
/// consumed by <c>RustArchon.Worker</c>'s <c>TicketEventConsumer</c> - the seam a site-owner-configured
/// external ticketing integration hangs off, entirely separate from the ticket's own notification email
/// (<c>EmailTemplateRegistry.Codes.TicketReceived</c>), which is queued directly by the controller, not
/// through this event.
/// </summary>
/// <remarks>
/// Competing-consumer, like <see cref="EmailRequested"/> - exactly one live Worker instance should
/// mirror this event out, not all of them.
/// </remarks>
public record TicketCreated(Guid TicketId);
