// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// "A message was added to a ticket's thread." Published by both <c>TicketsController</c> (a customer
/// reply) and <c>AdminTicketsController</c> (a staff reply) - never for a <c>TicketNote</c>, which is
/// this system's own internal working notes and stays out of the mirrored event stream regardless of
/// which ticketing provider is configured. See <see cref="TicketCreated"/>'s remarks.
/// </summary>
public record TicketMessageAdded(Guid TicketId, Guid MessageId);
