// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// "Send this email." Published by <c>RustArchon.Api</c>'s <c>InternalController</c> (on behalf of
/// the Blazor web app, which has no direct broker access - see its remarks) and consumed by
/// <c>RustArchon.Worker</c>'s <c>EmailRequestedConsumer</c>.
/// </summary>
/// <remarks>
/// Competing-consumer, like <see cref="ConnectToServer"/> - any one live Worker instance sending an
/// email exactly once is exactly what's wanted here, unlike the fanout messages
/// (<see cref="ServerLifecycleChanged"/>, <see cref="SendRconCommand"/>) that every instance needs its
/// own copy of. This is MassTransit's default topology for a plain <c>IConsumer&lt;T&gt;</c>, so no
/// special queue configuration is needed for that - see <c>EmailRequestedConsumer</c>'s receive
/// endpoint registration in <c>RustArchon.Worker</c>'s <c>Program.cs</c> for the retry policy that
/// *is* configured there.
/// </remarks>
/// <param name="MessageId">
/// A stable id for this specific email request, minted by the publisher. Not currently persisted
/// anywhere (there's no audit-trail entity yet) - it exists so log lines from the publish side and the
/// consume side can be correlated by grepping for one value, and so a future audit trail has an
/// obvious natural key to key off without a schema change.
/// </param>
public record EmailRequested(Guid MessageId, string To, string Subject, string HtmlBody);
