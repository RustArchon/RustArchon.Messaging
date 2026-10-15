// Copyright ©2026 Scott Blomfield

namespace RustArchon.Messaging.Contracts;

/// <summary>
/// Published on a timer by each Worker instance with the log entries it has produced since the last batch, so the Api can serve them through
/// the diagnostic log endpoint (ADR-0006). Best effort and at-most-once: a batch that fails to publish is dropped, and the sequence numbers make
/// the loss visible to whoever reads the logs.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="Entries"/> are already redacted by the Worker and carry the sequence numbers the Worker assigned (<see cref="WorkerLogEntry.Seq"/>
/// rises by exactly one per entry within one <see cref="InstanceId"/>). Several Worker instances can run at once, each with its own
/// <see cref="InstanceId"/>; a restarted Worker is a new instance. This is not a <b>fanout</b> message: it goes to one Api consumer, which is
/// the supported topology today (one Api instance).
/// </para>
/// <para>
/// This is distinct from <see cref="WorkerDiagnosticLogged"/>, which feeds a per-server connection log tab in the Panel and is persisted.
/// Nothing here is persisted.
/// </para>
/// </remarks>
public record WorkerLogBatch(string InstanceId, IReadOnlyList<WorkerLogEntry> Entries);

/// <summary>One captured Worker log entry. <see cref="Level"/> is a <c>Microsoft.Extensions.Logging.LogLevel</c> value (Debug = 1 … Critical = 5).</summary>
public record WorkerLogEntry(
    long Seq,
    DateTimeOffset TimestampUtc,
    int Level,
    string Category,
    string Message,
    string? Exception,
    IReadOnlyDictionary<string, string> Properties);
