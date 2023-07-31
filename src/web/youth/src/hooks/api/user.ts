import { UseQueryResult, useQuery } from "@tanstack/react-query";
import { User, UserProfileRequest } from "~/api/models/user";
import { getUser, patchUser } from "~/api/user";
import { useHttpAuth } from "../useHttpAuth";

export const useGetUser = (): UseQueryResult<User> => {
  const { session } = useHttpAuth();

  return useQuery(["getUser"], () => getUser(), {
    enabled: !!session?.user.id,
  });
};

export const usePatchUser = (
  model: UserProfileRequest | null
): UseQueryResult<User> => {
  const { session } = useHttpAuth();

  return useQuery(["patchUser"], () => patchUser(model!), {
    enabled: !!session?.user.id && model != null,
  });
};
