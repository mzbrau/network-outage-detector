---
title: Outage Detection
sidebar_position: 4
slug: /outage-detection
description: Learn the exact algorithm used to declare, time, and close an outage window.
---

# Outage Detection

## Detection model

The detector treats each probe cycle as a single health decision:

1. Ping every target in parallel.
2. Mark the cycle as successful if **any** target responds.
3. Mark the cycle as failed only if **every** target fails.

This makes the tool resilient to isolated endpoint issues while still detecting a true loss of connectivity.

## Confirming an outage

An outage is created when the application sees three failed cycles in a row.

Important detail: the recorded outage start time is the timestamp of the **first** failed cycle, not the third. That means the duration reflects the likely beginning of the interruption rather than the time confirmation was announced.

## Ending an outage

An outage closes on the first successful cycle after it begins. The detector records the end timestamp immediately and logs the resolved duration.

## Ongoing outages

If the process is stopped during an active outage, shutdown handling closes the outage using the current UTC time and includes it in the session summary.

## Time calculations

The detector stores outage times in UTC internally and converts to local time for user-facing output. This keeps calculations consistent while still making logs easy to read.
