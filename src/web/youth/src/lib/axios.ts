import axios from "axios";
import { type Session } from "next-auth";
import { getSession } from "next-auth/react";
import { env } from "~/env.mjs";

// const http = axios.create({
//   baseURL: env.NEXT_PUBLIC_API_BASE_URL,
// });

// const setAuthToken = (token: string) => {
//   if (!!token) {
//     http.defaults.headers.common.Authorization = `Bearer ${token}`;
//   } else {
//     delete http.defaults.headers.common.Authorization;
//   }
// };

// export { http, setAuthToken };

const ApiClient = () => {
  const instance = axios.create({
    baseURL: env.NEXT_PUBLIC_API_BASE_URL,
  });

  var lastSession: Session | null = null;

  instance.interceptors.request.use(
    async (request) => {
      if (lastSession == null || Date.now() > Date.parse(lastSession.expires)) {
        console.log("interceptor: NEW SESSION");
        const session = await getSession();
        lastSession = session;
      } else {
        console.log("interceptor: EXISTING SESSION");
      }

      console.log(JSON.stringify(lastSession));

      console.log(
        "interceptor: now:" +
          Date.now() +
          " expires:" +
          Date.parse(lastSession?.expires as string)
      );

      if (lastSession) {
        request.headers.Authorization = `Bearer ${lastSession.accessToken}`;
      } else {
        request.headers.Authorization = undefined;
        //delete instance.defaults.headers.common.Authorization;
      }

      // request.headers = {
      //   ...request.headers,
      //   Authorization: token,
      // };

      return request;
    },
    (error) => {
      console.error(`API Error: `, error);
      throw error;
    }
  );

  return instance;
};

export default ApiClient();
