---
title: Development and Release
sidebar_position: 7
slug: /development-and-release
description: Build, test, release, and publish both the application and this documentation site.
---

# Development and Release

## Repository layout

- `/NetworkOutageDetector` contains the .NET application source.
- `/.github/workflows/release.yml` publishes tagged release artifacts.
- `/docs` contains the Docusaurus documentation site.

## Application validation

Use the existing .NET commands from the repository root:

```bash
dotnet build
dotnet test
```

The current repository does not include a dedicated test project, so `dotnet test` serves as a restore and verification step for the solution.

## Documentation site workflow

From the `/docs` directory you can run:

```bash
npm install
npm run build
npm run start
```

Use `npm run start` for local authoring and `npm run build` to verify the production site output.

## Release workflow

The release workflow triggers on git tags matching `v*` and publishes self-contained binaries for the supported runtime identifiers.

## Documentation deployment

The GitHub Pages workflow is configured to deploy the Docusaurus site when documentation content under `/docs` changes on the `main` branch.
