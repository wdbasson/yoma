import { http } from "~/lib/axios";
import { User } from "./models/user";

export const getUser = async (): Promise<User> => {
  const { data } = await http.get("/user");
  return data;
};

export const patchUser = async (model: User): Promise<User> => {
  const { data } = await http.patch("/user", model);
  return data;
};
