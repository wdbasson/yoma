import { getSession } from "next-auth/react";
import { useRouter } from "next/router";
import { useCallback, type ReactElement } from "react";
import { IoMdPerson, IoMdSettings } from "react-icons/io";
import MainBackButton from "~/components/Layout/MainBackButton";
import { useHttpAuth } from "~/hooks/useHttpAuth";
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
      <div className="container-centered">
        <div className="container-content">
          <div className="flex flex-col items-center">
            <div className="relative h-11 w-11 cursor-pointer overflow-hidden rounded-full border-2 hover:border-white">
              <IoMdPerson className="absolute -left-1 h-12 w-12 text-gray-400 animate-in slide-in-from-top-4" />
            </div>
            <h1>Sam Henderson</h1>
            <h2>South Africa</h2>
          </div>

          <div className="flex flex-col items-start gap-2">
            <h1>Wallet</h1>
            <ul className="list-disc pl-8">
              <li>100 $YOMA</li>
              <li>50 $ZLTO</li>
            </ul>
            <div className="flex gap-2 py-4">
              <button className="btn btn-primary">Receive</button>
              <button className="btn btn-primary">Send</button>
            </div>
          </div>

          <div className="flex flex-col items-start gap-2">
            <h1>Skills</h1>
            <ul className="list-disc pl-8">
              <li>Project Management</li>
              <li>3D Mapping</li>
            </ul>
            <div className="flex gap-2 py-4">
              <button className="btn btn-primary">Add to</button>
              <button className="btn btn-primary">Share</button>
            </div>
          </div>

          <div className="flex flex-col items-start gap-2">
            <h1>Achievements</h1>
            <ul className="list-disc pl-8">
              <li>Birth Registration</li>
              <li>Plant 100 Trees</li>
            </ul>
            <div className="flex gap-2 py-4">
              <button className="btn btn-primary">Add to</button>
              <button className="btn btn-primary">Share</button>
            </div>
          </div>
        </div>
      </div>
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

const Settings: NextPageWithLayout = () => {
  const router = useRouter();

  const handleClick = useCallback(() => {
    router.push("/user/settings");
  }, [router]);

  return (
    <button
      type="button"
      aria-label="Close"
      className="btn-hover-grow btn btn-square gap-2 border-none bg-transparent hover:border-none hover:bg-transparent"
      onClick={handleClick}
    >
      <IoMdSettings className="h-6 w-6" />
    </button>
  );
};

UserProfile.getLayout = function getLayout(page: ReactElement) {
  return (
    <MainBackButton rightMenuChildren={<Settings />}>{page}</MainBackButton>
  );
};

export default UserProfile;
