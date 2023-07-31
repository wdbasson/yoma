//hooks/api/posts.js

import { UseQueryResult, useQuery } from "@tanstack/react-query";
import * as api from "~/api/lookups";
import { Country, Gender } from "~/api/models/lookups";

export const useGenders = (): UseQueryResult<Gender[]> => {
  return useQuery(["genders"], () => api.getGenders());
};

export const useCountries = (): UseQueryResult<Country[]> => {
  return useQuery(["countries"], () => api.getCountries());
};
