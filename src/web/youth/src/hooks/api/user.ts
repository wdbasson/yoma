//hooks/api/posts.js

import { useQuery } from "@tanstack/react-query";
import * as api from "~/api/user";

export const useUserProfile = (email: string) => {
  return useQuery(["userProfile", email], () => api.getUserProfile(email), {
    enabled: email != null,
  });
};
