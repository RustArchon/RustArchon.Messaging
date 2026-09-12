// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Request/response pair for "send one test email right now, using whatever provider is currently
/// configured, and tell me whether it worked" - the admin settings page's way of letting a site admin
/// verify their SMTP/SendGrid setup without guessing from Worker logs.
/// </summary>
/// <remarks>
/// <para>
/// A request/response pair, not a fire-and-forget publish like <see cref="EmailRequested"/> - the whole
/// point is a definite pass/fail the admin sees immediately, the same reasoning
/// <see cref="SendRconCommand"/>/<see cref="RconCommandResult"/> already established for "ask a Worker
/// to do something and wait for a real answer." See <c>PlatformSettingsController.TestEmail</c> in
/// <c>RustArchon.Api</c> (the request client caller) and <c>SendTestEmailConsumer</c> in
/// <c>RustArchon.Worker</c> (the responder).
/// </para>
/// <para>
/// Competing-consumer, like <see cref="EmailRequested"/> - exactly one live Worker instance should
/// attempt the send, not every one of them answering separately.
/// </para>
/// </remarks>
public record SendTestEmail(string To, string Subject, string HtmlBody);

/// <summary>
/// Response to a <see cref="SendTestEmail"/> request.
/// </summary>
/// <remarks>
/// <see cref="Error"/> is populated only when <see cref="Success"/> is <c>false</c> - the delivery
/// provider's own exception message, so the admin sees why it failed (bad credentials, unreachable
/// host, ...) rather than a bare "no."
/// </remarks>
public record SendTestEmailResult(bool Success, string? Error, string ProviderName);
