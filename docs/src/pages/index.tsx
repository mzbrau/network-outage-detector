import type {ReactNode} from 'react';
import Link from '@docusaurus/Link';
import Layout from '@theme/Layout';
import Heading from '@theme/Heading';
import styles from './index.module.css';

const highlights = [
  {
    title: 'Reliable outage detection',
    description:
      'Outages are recorded only after three consecutive rounds where every target fails, keeping one-off host hiccups out of your reports.',
  },
  {
    title: 'Operator-friendly reporting',
    description:
      'Timestamped console output, rotating daily log files, clock-hour summaries, and shutdown recaps make the detector easy to review later.',
  },
  {
    title: 'Simple deployment path',
    description:
      'Run it locally, publish self-contained binaries from GitHub Releases, and keep this documentation site live through GitHub Pages.',
  },
];

const docCards = [
  {
    title: 'Get started',
    href: '/docs/getting-started',
    description: 'Install the .NET 10 SDK, launch the monitor, and verify the first run.',
  },
  {
    title: 'Tune the monitor',
    href: '/docs/configuration',
    description: 'Review supported CLI arguments, defaults, and the target selection strategy.',
  },
  {
    title: 'Operate with confidence',
    href: '/docs/operations',
    description: 'Learn how to interpret logs, place the detector, and respond during outages.',
  },
];

export default function Home(): ReactNode {
  return (
    <Layout
      title="Documentation"
      description="Documentation for the Network Outage Detector .NET application.">
      <header className={styles.hero}>
        <div className="container">
          <div className={styles.heroGrid}>
            <div className={styles.heroCopy}>
              <span className={styles.badge}>Open-source .NET 10 monitor</span>
              <Heading as="h1" className={styles.heroTitle}>
                Know exactly when the network dropped.
              </Heading>
              <p className={styles.heroSubtitle}>
                Network Outage Detector continuously probes multiple targets, records precise outage windows,
                and produces the operational summaries needed for later analysis.
              </p>
              <div className={styles.heroActions}>
                <Link className="button button--primary button--lg" to="/docs/overview">
                  Read the docs
                </Link>
                <Link className="button button--secondary button--lg" to="https://github.com/mzbrau/network-outage-detector">
                  View repository
                </Link>
              </div>
              <div className={styles.statRow}>
                <div className={styles.statCard}>
                  <strong>3 failures</strong>
                  <span>to confirm an outage</span>
                </div>
                <div className={styles.statCard}>
                  <strong>1 second</strong>
                  <span>between probe rounds</span>
                </div>
                <div className={styles.statCard}>
                  <strong>Daily logs</strong>
                  <span>with graceful fallback</span>
                </div>
              </div>
            </div>
            <div className={styles.heroPanel}>
              <div className={styles.panelHeader}>
                <span>Live timeline</span>
                <span className={styles.panelStatus}>Monitoring</span>
              </div>
              <div className={styles.timelineItem}>
                <span>14:05:21</span>
                <p>All configured targets failed for the first time.</p>
              </div>
              <div className={styles.timelineItem}>
                <span>14:05:23</span>
                <p>Failure threshold reached and outage window opened.</p>
              </div>
              <div className={styles.timelineItem}>
                <span>14:06:10</span>
                <p>Connectivity restored and the outage duration finalized.</p>
              </div>
              <div className={styles.panelFooter}>
                <div>
                  <strong>98.6%</strong>
                  <span>Example hourly uptime</span>
                </div>
                <div>
                  <strong>49s</strong>
                  <span>Example downtime</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </header>

      <main className={styles.mainContent}>
        <section className="container margin-top--lg margin-bottom--xl">
          <div className={styles.sectionHeading}>
            <Heading as="h2">What the detector gives you</Heading>
            <p>Focused documentation and an intentionally simple runtime model.</p>
          </div>
          <div className={styles.cardGrid}>
            {highlights.map((item) => (
              <article key={item.title} className={styles.featureCard}>
                <Heading as="h3">{item.title}</Heading>
                <p>{item.description}</p>
              </article>
            ))}
          </div>
        </section>

        <section className={`container margin-bottom--xl ${styles.docSection}`}>
          <div className={styles.sectionHeading}>
            <Heading as="h2">Start with the right guide</Heading>
            <p>Everything below is written specifically for this repository and its current behavior.</p>
          </div>
          <div className={styles.docGrid}>
            {docCards.map((item) => (
              <Link key={item.title} className={styles.docCard} to={item.href}>
                <Heading as="h3">{item.title}</Heading>
                <p>{item.description}</p>
                <span>Open guide →</span>
              </Link>
            ))}
          </div>
        </section>
      </main>
    </Layout>
  );
}
