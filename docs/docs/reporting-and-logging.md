---
title: Reporting and Logging
sidebar_position: 5
slug: /reporting-and-logging
description: See how console output, file logs, hourly summaries, and shutdown reports are produced.
---

# Reporting and Logging

## Timestamped output

Every log entry is prefixed with a local timestamp in this format:

```text
[YYYY-MM-DD HH:mm:ss]
```

This applies to startup events, outage transitions, hourly summaries, and shutdown output.

## Daily log files

The detector attempts to append each line to a daily file in the current working directory:

```text
outage-log-YYYY-MM-DD.txt
```

When the date changes, the writer rotates automatically to the new file.

## Graceful degradation

If file logging becomes unavailable because of an I/O or permissions issue, the detector prints a warning and continues with console-only logging instead of terminating.

## Hourly reports

Hourly reports are aligned to real clock hours rather than fixed 60-minute windows from launch time.

Each report includes:

- The time window covered
- Uptime percentage for that window
- Total downtime in seconds for that window

The first report starts at session launch and ends at the next clock-hour boundary.

## Session summary

When the application exits, it prints a final summary containing:

- Total outage count
- Total downtime in seconds

This provides a quick roll-up for the full monitoring session.
