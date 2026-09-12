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
public record CommunicationDelivered(Guid CommunicationId, bool Success, string? Error);
