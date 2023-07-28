import { useSession } from "next-auth/react";
import { setAuthToken } from "~/lib/axios";

// sets the auth token on the axios instance
export const useHttpAuth = () => {
  const { data, status } = useSession();

  if (status !== "loading") {
    if (status === "authenticated") {
      setAuthToken(data.accessToken);
    } else if (status === "unauthenticated") {
      setAuthToken("");
    }
  }

  return {
    session: data,
    isLoading: status === "loading",
    isAuthenticated: status === "authenticated",
  };
};
