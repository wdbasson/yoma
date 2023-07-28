import { http } from "~/lib/axios";

export const getUserProfile = async (email: string) => {
  const { data } = await http.get("/users/email/" + email);
  return data;
};
