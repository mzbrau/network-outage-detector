---
title: Configuration
sidebar_position: 3
slug: /configuration
description: Review the current runtime defaults, supported arguments, and target selection guidance.
---

# Configuration

## Supported inputs

The current CLI supports a compact argument surface:

| Input | Purpose | Example |
| --- | --- | --- |
| `--targets <addr,addr,...>` | Pass a comma-separated target list | `--targets 8.8.8.8,1.1.1.1` |
| Positional arguments | Pass one or more targets as separate values | `8.8.8.8 1.1.1.1` |

If both forms are omitted, the detector uses its built-in defaults.

## Runtime defaults

| Setting | Value |
| --- | --- |
| Probe interval | 1 second |
| Ping timeout | 500 ms |
| Outage threshold | 3 consecutive all-target failures |
| Default targets | `8.8.8.8`, `1.1.1.1` |

These values are currently defined in code and are not exposed as configurable settings.

## Target selection guidance

Choose targets that reflect the network path you care about:

- Use multiple targets so a single remote host failure does not create a false outage.
- Prefer stable infrastructure endpoints when monitoring internet access.
- Include addresses on different upstream services when possible.
- Avoid targets that are rate-limited, blocked by firewalls, or operationally noisy.

## Input validation behavior

- Empty values in the `--targets` comma-separated list are discarded.
- If no valid targets remain after parsing, the program exits with an error.
- Positional arguments ignore option-like values that begin with `-`.

## What is not configurable today

The following behavior is fixed in the current implementation:

- Failure threshold
- Hourly report cadence
- Log filename format
- Probe timeout and interval

If those settings need to vary per environment, update the application before relying on it as a managed service.
