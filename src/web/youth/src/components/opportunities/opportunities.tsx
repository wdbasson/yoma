import { dehydrate, QueryClient } from "@tanstack/react-query";
import { getOpportunities } from "~/api/opportunities";

export const getServerSideProps = async (ctx: any) => {
  const { id } = ctx.params;

  const queryClient = new QueryClient();

  // prefetch data on the server
  await queryClient.fetchQuery(["post", id], () => getOpportunities(id));

  return {
    props: {
      // dehydrate query cache
      dehydratedState: dehydrate(queryClient),
    },
  };
};
