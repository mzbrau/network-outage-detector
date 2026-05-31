---
title: Getting Started
sidebar_position: 2
slug: /getting-started
description: Install the prerequisites, run the detector, and verify its first successful monitoring session.
---

# Getting Started

## Prerequisites

To run from source, install the [.NET 10 SDK](https://dotnet.microsoft.com/download).

## Run from source

```bash
git clone https://github.com/mzbrau/network-outage-detector.git
cd network-outage-detector/NetworkOutageDetector
dotnet run
```

The application starts monitoring immediately and prints the configured targets at startup.

## Use your own targets

```bash
dotnet run -- --targets 8.8.8.8,1.1.1.1,9.9.9.9
```

You can also provide positional arguments instead of the `--targets` option:

```bash
dotnet run -- 8.8.8.8 1.1.1.1
```

## Run a published binary

The repository release workflow creates self-contained builds for:

- Windows x64
- macOS x64
- macOS ARM64

Download the latest archive from the project Releases page, extract it, and run the executable directly.

## Verify the first session

A healthy startup looks like this:

```text
[2026-03-31 14:00:00] Network Outage Detector started. Targets: 8.8.8.8, 1.1.1.1
[2026-03-31 14:00:00] Pinging every 1s (timeout: 500ms, threshold: 3 failures)
[2026-03-31 14:00:00] Press Ctrl+C to stop.
```

Let it run long enough to cross an hour boundary if you want to confirm hourly reporting.

## Stop the detector

Press `Ctrl+C` for a graceful shutdown. The application finalizes any active outage, prints a session summary, and then exits.
