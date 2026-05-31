---
title: Overview
sidebar_position: 1
slug: /overview
description: Understand what Network Outage Detector does and what behavior to expect during monitoring.
---

# Overview

Network Outage Detector is a .NET 10 console application that watches connectivity by pinging multiple endpoints once per second and tracking when the entire set becomes unreachable.

## Core behavior

- The application pings every configured target in parallel.
- The network is considered **up** when at least one target responds successfully.
- The network is considered **down** only when **all** targets fail.
- An outage is confirmed after **three consecutive** all-target failures.
- The outage start time is recorded as the **first** failed probe in that sequence.
- The outage ends as soon as any configured target responds again.

## What you get while it runs

- Timestamped console output for each important lifecycle event.
- Daily rotating log files named `outage-log-YYYY-MM-DD.txt`.
- Hourly uptime and downtime summaries aligned to clock hours.
- A final session summary when the process shuts down.

## Default targets

If you do not pass any arguments, the detector monitors these public DNS endpoints:

- `8.8.8.8`
- `1.1.1.1`

These defaults give you immediate coverage without configuration, while still letting you replace them with your own monitoring targets.

## Good fit

Use the detector when you want a lightweight, continuously running signal for:

- Home or small office internet reliability
- Edge device connectivity monitoring
- Lightweight server or workstation uptime checks
- Historical outage timing that can be correlated with ISP incidents or router events
