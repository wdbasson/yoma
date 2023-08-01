import axios from "axios";
import { env } from "~/env.mjs";

const http = axios.create({
  baseURL: env.NEXT_PUBLIC_API_BASE_URL,
});

const setAuthToken = (token: string) => {
  if (!!token) {
    http.defaults.headers.common.Authorization = `Bearer ${token}`;
  } else {
    delete http.defaults.headers.common.Authorization;
  }
};

export { http, setAuthToken };
