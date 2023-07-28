import Head from "next/head";
import type { ReactElement } from "react";
import { NextAuthProvider } from "~/core/authProvider";
import { Footer } from "./footer";
import { Navbar } from "./navbar";

export type LayoutProps = ({
  children,
}: // title,
// backUrl,
// hideBackButton,
// maxWidth,
// darkBackground,
// classNames,
{
  children: ReactElement;
  // title?: string;
  // backUrl?: string;
  // hideBackButton?: boolean;
  // maxWidth?: string;
  // darkBackground?: boolean;
  // classNames?: string;
}) => ReactElement;

const MainLayout: LayoutProps = ({
  children,
  // title,
  // backUrl,
  // hideBackButton,
  // maxWidth,
  // darkBackground,
  // classNames,
}) => {
  return (
    <>
      <NextAuthProvider>
        <Head>
          <title>Yoma | Unlock Your Future</title>
          <meta name="viewport" content="initial-scale=1, width=device-width" />
          <meta
            name="description"
            content="The Yoma platform enables you to build and transform your future by unlocking your hidden potential. Make a difference, earn rewards and build your CV by taking part in our impact challenges."
          />
          <link rel="icon" href="/favicon.ico" />
        </Head>
        <Navbar />
        <main>{children}</main>
        <Footer />
      </NextAuthProvider>
    </>
  );
};

export default MainLayout;
