import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

const config: Config = {
  title: 'Network Outage Detector',
  tagline: 'Operational guidance for a lightweight .NET outage monitor.',
  favicon: 'img/favicon.ico',
  future: {
    v4: true,
  },
  url: 'https://mzbrau.github.io',
  baseUrl: '/network-outage-detector/',
  organizationName: 'mzbrau',
  projectName: 'network-outage-detector',
  trailingSlash: false,
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },
  presets: [
    [
      'classic',
      {
        docs: {
          routeBasePath: 'docs',
          sidebarPath: './sidebars.ts',
          editUrl: 'https://github.com/mzbrau/network-outage-detector/tree/main/',
        },
        blog: false,
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],
  themeConfig: {
    image: 'img/docusaurus-social-card.jpg',
    colorMode: {
      defaultMode: 'dark',
      disableSwitch: false,
      respectPrefersColorScheme: true,
    },
    navbar: {
      title: 'Network Outage Detector',
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'docsSidebar',
          position: 'left',
          label: 'Documentation',
        },
        {
          to: '/docs/getting-started',
          label: 'Get Started',
          position: 'left',
        },
        {
          href: 'https://github.com/mzbrau/network-outage-detector',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            {
              label: 'Overview',
              to: '/docs/overview',
            },
            {
              label: 'Operations',
              to: '/docs/operations',
            },
          ],
        },
        {
          title: 'Project',
          items: [
            {
              label: 'Repository',
              href: 'https://github.com/mzbrau/network-outage-detector',
            },
            {
              label: 'Releases',
              href: 'https://github.com/mzbrau/network-outage-detector/releases',
            },
          ],
        },
        {
          title: 'Build',
          items: [
            {
              label: 'Release workflow',
              href: 'https://github.com/mzbrau/network-outage-detector/blob/main/.github/workflows/release.yml',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Network Outage Detector. Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['bash', 'csharp'],
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
