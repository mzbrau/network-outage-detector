---
title: Operations
sidebar_position: 6
slug: /operations
description: Use operational practices that improve signal quality and make outage investigations easier.
---

# Operations

## Pick a stable runtime location

Run the detector from the same network segment that you want to observe. If you move it to another host or upstream path, the results describe that environment instead.

## Keep the working directory in mind

Daily log files are written to the current working directory. Choose a location where the process can create and append files, or intentionally run in a read-only location if console-only logging is acceptable.

## Interpret outages carefully

A recorded outage means all configured targets failed for three consecutive probe cycles. It does **not** automatically tell you whether the root cause was:

- ISP loss
- Local router failure
- Host firewall policy
- ICMP filtering upstream
- A shared issue affecting every configured target

Correlate detector output with router logs, ISP notices, and other monitoring sources when investigating incidents.

## Recommended operating habits

- Use at least two well-known targets.
- Keep the process running for long periods to collect meaningful hourly summaries.
- Preserve the daily log files if you need to compare patterns over time.
- Review the session summary before restarting or redeploying the process.

## Shutdown expectations

A graceful shutdown with `Ctrl+C` is the safest way to stop the detector because it finalizes reporting and closes the current session cleanly.
