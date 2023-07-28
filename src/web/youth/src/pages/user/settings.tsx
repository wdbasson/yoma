import { type ReactElement } from "react";
import MainBackButtonLayout from "~/components/Layout/MainBackButton";
import type { NextPageWithLayout } from "../_app";

const Settings: NextPageWithLayout = () => {
  //const { session } = useHttpAuth();

  return (
    <>
      <div className="container-centered">
        <div className="container-content">
          <h1>Settings</h1>
          <div>1111</div>
          <div>1111</div>
          <div>1111</div>
          <div>
            111111111111111111111111111 11111111111111111
            1111111111111111111111111111
          </div>
        </div>
      </div>
    </>
  );
};

Settings.getLayout = function getLayout(page: ReactElement) {
  return <MainBackButtonLayout>{page}</MainBackButtonLayout>;
};

export default Settings;
