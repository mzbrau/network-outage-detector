# Network Outage Detector

A .NET 10 console application that monitors network connectivity by pinging multiple targets and detecting outages. Designed to run continuously and report network disruptions with precise timestamps.

## Features

- **Multi-target pinging** — Pings multiple addresses in parallel (default: `8.8.8.8` and `1.1.1.1`). An outage is only registered when *all* targets fail, eliminating false positives from a single remote address going down.
- **Outage detection** — 3 consecutive all-target failures triggers an outage. The start time is recorded as the first failed ping, not the third.
- **Timestamped logging** — All output is prefixed with `[YYYY-MM-DD HH:mm:ss]`.
- **Hourly reports** — Clock-hour aligned uptime percentage and downtime statistics.
- **File logging** — Automatically writes to `outage-log-YYYY-MM-DD.txt` with daily rotation. Degrades gracefully to console-only if the directory is read-only.
- **Graceful shutdown** — Ctrl+C prints a session summary before exiting.

## Installation

### From GitHub Releases

Download the latest self-contained executable from [Releases](../../releases):

| Platform | File |
|----------|------|
| Windows x64 | `NetworkOutageDetector-win-x64.zip` |
| macOS x64 (Intel) | `NetworkOutageDetector-osx-x64.zip` |
| macOS ARM64 (Apple Silicon) | `NetworkOutageDetector-osx-arm64.zip` |

Extract and run — no .NET runtime installation required.

### From Source

Requires [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/mzbrau/network-outage-detector.git
cd network-outage-detector/NetworkOutageDetector
dotnet run
```

## Usage

```bash
# Default: ping 8.8.8.8 and 1.1.1.1
dotnet run

# Custom targets (comma-separated)
dotnet run -- --targets 8.8.8.8,1.1.1.1,9.9.9.9

# Positional arguments
dotnet run -- 8.8.8.8 1.1.1.1

# Self-contained executable
./NetworkOutageDetector
./NetworkOutageDetector --targets 8.8.8.8,1.0.0.1
```

### CLI Options

| Option | Description | Default |
|--------|-------------|---------|
| `--targets <addr,addr,...>` | Comma-separated list of addresses to ping | `8.8.8.8,1.1.1.1` |
| Positional args | Space-separated addresses | `8.8.8.8 1.1.1.1` |

## Sample Output

```
[2026-03-31 14:00:00] Network Outage Detector started. Targets: 8.8.8.8, 1.1.1.1
[2026-03-31 14:00:00] Pinging every 1s (timeout: 500ms, threshold: 3 failures)
[2026-03-31 14:00:00] Press Ctrl+C to stop.

[2026-03-31 14:05:23] ⚠ OUTAGE STARTED
[2026-03-31 14:06:10] ✓ OUTAGE ENDED   | Start: 14:05:21 | End: 14:06:10 | Duration: 49s

[2026-03-31 15:00:00] ── Hourly Report (14:00–15:00) ──────────────────
[2026-03-31 15:00:00]   Uptime: 98.6% | Downtime: 49s
[2026-03-31 15:00:00] ─────────────────────────────────────────────────

[2026-03-31 15:30:00] ── Session Summary ─────────────────────────────
[2026-03-31 15:30:00]   Total outages: 1
[2026-03-31 15:30:00]   Total downtime: 49s
[2026-03-31 15:30:00] ─────────────────────────────────────────────────
[2026-03-31 15:30:00] Shutting down.
```

## Versioning

This project uses [MinVer](https://github.com/adamralph/minver) for automatic versioning from git tags. Create a tag to set the version:

```bash
git tag v1.0.0
git push origin v1.0.0
```

This triggers a GitHub Actions release with self-contained builds for Windows and macOS.

## Documentation

Project documentation now lives in the [Docusaurus site source](docs/README.md). To preview it locally:

```bash
cd docs
npm install
npm run start
```

## License

See [LICENSE](LICENSE) for details.