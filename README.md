# RustArchon.Messaging

MassTransit message contracts shared between
[RustArchon.Api](https://github.com/RustArchon/RustArchon.Api) and
[RustArchon.Worker](https://github.com/RustArchon/RustArchon.Worker) (RabbitMQ is the transport).
Plain record types - `ConnectToServer`, `ServerLifecycleChanged`, `ServerConnectionHeartbeat`,
`RconFrameCaptured`, `ConnectionStatusChanged`, `SendRconCommand`/`RconCommandResult` - with no
MassTransit types or other dependencies of its own.

Part of the [RustArchon](https://github.com/RustArchon/RustArchon) system - see that repo for the
full architecture and how to run the whole stack locally or via Docker Compose.

## License

AGPL-3.0-or-later - see [`LICENSE`](LICENSE). See [`NOTICE.md`](NOTICE.md) for how this project
relates to [JumpStart](https://github.com/cyberknet/JumpStart) elsewhere in the RustArchon system
(this project itself has no JumpStart dependency).

## Building standalone

This repo has no dependencies at all and builds fully standalone:

```bash
git clone https://github.com/RustArchon/RustArchon.Messaging.git
cd RustArchon.Messaging
dotnet build
```
