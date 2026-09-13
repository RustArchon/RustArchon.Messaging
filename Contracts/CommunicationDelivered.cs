// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// "Here's what happened when I tried to send that." Published by <c>RustArchon.Worker</c>'s
/// <c>EmailRequestedConsumer</c> after it attempts delivery for one <see cref="EmailRequested"/>, and
/// consumed by <c>RustArchon.Api</c>'s <c>CommunicationDeliveredConsumer</c> to move the matching
/// <c>Communication</c> row from Queued to Sent or Bounced.
/// </summary>
/// <remarks>
/// Fire-and-forget, unlike <see cref="SendTestEmail"/>'s request/response - nobody is waiting
/// synchronously for a real production send the way an admin waits on a test one, so there's no
/// caller to respond to. <see cref="CommunicationId"/> is the same id as the
/// <see cref="EmailRequested.MessageId"/> it answers, which is itself the <c>Communication</c> row's
/// own id - see that record's remarks.
/// </remarks>
/// <param name="Suppressed">
/// True when the sending Worker instance had <c>RUSTARCHON_SUPPRESS_EMAIL_DELIVERY</c> set - nothing
/// was actually sent, deliberately, regardless of <paramref name="Success"/>. Checked first by
/// <c>CommunicationDeliveredConsumer</c>, which moves the row to a distinct
/// <c>CommunicationStatus.Suppressed</c> rather than <c>Sent</c> - the row and its full body were still
/// queued and saved normally either way; only the delivery attempt itself was skipped. See
/// <c>SuppressedEmailDeliveryProvider</c>'s remarks for why this needs to be told apart from an
/// ordinary successful send.
/// </param>
public record CommunicationDelivered(Guid CommunicationId, bool Success, string? Error, bool Suppressed);
