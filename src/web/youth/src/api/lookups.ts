import { http } from "~/lib/axios";
import type { Country, Gender } from "./models/lookups";

export const getGenders = async (): Promise<Gender[]> => {
  const { data } = await http.get<Gender[]>("/lookup/gender");
  return data;
};

export const getCountries = async (): Promise<Country[]> => {
  const { data } = await http.get<Country[]>("/lookup/country");
  return data;
};
