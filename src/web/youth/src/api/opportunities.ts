import axios from "~/lib/axios";

export const getOpportunities = async (id: string) => {
  const { data } = await axios.get("/opportunities");
  return data;
};
