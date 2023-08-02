import type { UseQueryResult } from "@tanstack/react-query";
import { useQuery } from "@tanstack/react-query";
import type { User } from "~/api/models/user";
import { getUser } from "~/api/user";

export const useGetUser = (): UseQueryResult<User> => {
  return useQuery(["getUser"], () => getUser());
};
