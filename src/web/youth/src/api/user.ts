import { http } from "~/lib/axios";
import { User, UserProfileRequest } from "./models/user";

export const getUser = async (): Promise<User> => {
  const { data } = await http.get("/user");
  return data;
};

export const patchUser = async (model: UserProfileRequest): Promise<User> => {
  const { data } = await http.patch("/user", model);
  return data;
};
