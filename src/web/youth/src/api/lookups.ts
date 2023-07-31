import { http } from "~/lib/axios";
import { Country, Gender } from "./models/lookups";

export const getGenders = async (): Promise<Gender[]> => {
  const { data } = await http.get("/lookup/gender");
  return data;
};

export const getCountries = async (): Promise<Country[]> => {
  const { data } = await http.get("/lookup/country");
  return data;
};
