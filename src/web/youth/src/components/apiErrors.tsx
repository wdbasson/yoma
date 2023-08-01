import { type AxiosError } from "axios";
import { ReactElement } from "react";
import { IoMdAlert } from "react-icons/io";
import { type ErrorResponseItem } from "~/api/models/common";

export type Props = ({ error }: { error: AxiosError }) => ReactElement;

export const ApiErrors: Props = ({ error }) => {
  const customErrors = error.response?.data as ErrorResponseItem[];

  return (
    <div className="px-4">
      {(() => {
        switch (error.response?.status) {
          case 401:
            return (
              <>
                <div className="mb-4 flex flex-row items-center text-sm font-bold">
                  <IoMdAlert className="mr-2 text-xl text-yellow-400" /> Access
                  Denied
                </div>
                <p className="text-sm">
                  Your session has expired. Please sign-in and try again.
                </p>
              </>
            );
          case 403:
            return (
              <>
                <div className="mb-4 flex flex-row items-center text-sm font-bold">
                  <IoMdAlert className="mr-2 text-xl text-yellow-400" /> Access
                  Denied
                </div>
                <p className="text-sm">
                  You don't have access to perform this action. Please contact
                  us to request access.
                </p>
              </>
            );
          case 500:
            return (
              <>
                <div className="mb-4 flex flex-row items-center text-sm font-bold">
                  <IoMdAlert className="mr-2 text-xl text-red-400" /> Access
                  Denied
                </div>
                <p className="text-sm">
                  An unknown error has occurred. Please contact us or try again
                  later. ☹️
                </p>
              </>
            );
          default:
            if (customErrors?.length === 0) {
              return (
                <>
                  <div className="mb-4 flex flex-row items-center text-sm font-bold">
                    <IoMdAlert className="mr-2 text-xl text-red-400" /> Access
                    Denied
                  </div>
                  <p className="text-sm">
                    An unknown error has occurred. Please contact us or try
                    again later. ☹️
                  </p>
                </>
              );
            }
            if (customErrors.length === 1) {
              return (
                <div className="mb-4 flex flex-row items-center text-sm font-bold">
                  <IoMdAlert className="mr-2 text-xl text-red-400" />
                  {customErrors[0]?.message}
                </div>
              );
            }
            if (customErrors.length > 1) {
              return (
                <>
                  <div className="mb-4 flex flex-row items-center text-sm font-bold">
                    <IoMdAlert className="mr-2 text-xl text-red-400" /> The
                    following errors occurred:
                  </div>
                  <ul className="list-disc">
                    {customErrors?.map((error) => (
                      <li className="text-sm">{error.message}</li>
                    ))}
                  </ul>
                </>
              );
            }
            return null;
        }
      })()}
    </div>
  );
};
