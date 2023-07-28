import { getSession } from "next-auth/react";
import { type ReactElement } from "react";
import { useHttpAuth } from "~/hooks/useHttpAuth";
import MainLayout from "../../components/layout";
import type { NextPageWithLayout } from "../_app";

const UserProfile: NextPageWithLayout = () => {
  const { session } = useHttpAuth();

  // const { data: userProfile, isLoading } = useUserProfile(
  //   session?.user?.email!
  // );

  //if (isLoading) return <Loader />;

  const handleClick = () => {
    var session = getSession();
  };

  return (
    <>
      <main className="flex min-h-screen flex-col items-center justify-center bg-gradient-to-b from-[#2e026d] to-[#15162c]">
        <div className="container flex flex-col items-center justify-center gap-12 px-4 py-16 ">
          <h1 className="text-5xl font-extrabold tracking-tight text-white sm:text-[5rem]">
            Hello
          </h1>
          {/* <div>userProfile: {JSON.stringify(userProfile)}</div> */}
          {<div>session: {JSON.stringify(session?.user.profile)}</div>}

          <button onClick={handleClick}>Refresh</button>
        </div>
      </main>
    </>
  );

  // return (
  //   <>
  //     {/* TODO: override title/keywords etc for this page*/}
  //     {/* <Head>
  //       <title>Yoma | Unlock Your Future</title>
  //       <meta name="viewport" content="initial-scale=1, width=device-width" />
  //       <meta
  //         name="description"
  //         content="The Yoma platform enables you to build and transform your future by unlocking your hidden potential. Make a difference, earn rewards and build your CV by taking part in our impact challenges."
  //       />
  //       <link rel="icon" href="/favicon.ico" />
  //     </Head> */}

  //     {/* <main className="flex min-h-screen flex-col items-center justify-center bg-gradient-to-b from-[#2e026d] to-[#15162c]">
  //       <div className="container flex flex-col items-center justify-center gap-12 px-4 py-16 ">
  //         <h1 className="text-5xl font-extrabold tracking-tight text-white sm:text-[5rem]">
  //           Hello {session?.user?.name}
  //         </h1>
  //         <h2 className="text-5xl font-extrabold tracking-tight text-white sm:text-[5rem]">
  //           Update <span className="text-[hsl(280,100%,70%)]">profile</span>
  //         </h2>
  //         <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:gap-8">
  //           <div>{JSON.stringify(session)}</div>
  //           <Link
  //             className="flex max-w-xs flex-col gap-4 rounded-xl bg-white/10 p-4 text-white hover:bg-white/20"
  //             href="https://create.t3.gg/en/usage/first-steps"
  //             target="_blank"
  //           >
  //             <h3 className="text-2xl font-bold">First Steps →</h3>
  //             <div className="text-lg">
  //               Just the basics - Everything you need to know to set up your
  //               database and authentication.
  //             </div>
  //           </Link>
  //           <Link
  //             className="flex max-w-xs flex-col gap-4 rounded-xl bg-white/10 p-4 text-white hover:bg-white/20"
  //             href="https://create.t3.gg/en/introduction"
  //             target="_blank"
  //           >
  //             <h3 className="text-2xl font-bold">Documentation →</h3>
  //             <div className="text-lg">
  //               Learn more about Create T3 App, the libraries it uses, and how
  //               to deploy it.
  //             </div>
  //           </Link>
  //         </div>
  //       </div>
  //     </main> */}
  //   </>
  // );
};

UserProfile.getLayout = function getLayout(page: ReactElement) {
  return <MainLayout>{page}</MainLayout>;
};

export default UserProfile;
