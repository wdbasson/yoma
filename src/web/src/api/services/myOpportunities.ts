import ApiClient from "~/lib/axiosClient";
import type { Opportunity } from "../models/opportunity";
import type {
  MyOpportunityRequestVerify,
  MyOpportunityRequestVerifyFinalizeBatch,
  MyOpportunityResponseVerify,
  MyOpportunityResponseVerifyFinalizeBatch,
  MyOpportunitySearchCriteriaOpportunity,
  MyOpportunitySearchFilter,
  MyOpportunitySearchFilterAdmin,
  MyOpportunitySearchResults,
  VerificationStatus,
} from "../models/myOpportunity";
import { objectToFormData } from "~/lib/utils";
import type {
  GetServerSidePropsContext,
  GetStaticPropsContext,
} from "next/types";
import ApiServer from "~/lib/axiosServer";

export const saveMyOpportunity = async (
  opportunityId: string,
): Promise<Opportunity> => {
  const { data } = await (
    await ApiClient
  ).put<Opportunity>(`/myopportunity/action/${opportunityId}/save`);
  return data;
};

export const removeMySavedOpportunity = async (
  opportunityId: string,
): Promise<Opportunity> => {
  const { data } = await (
    await ApiClient
  ).delete<Opportunity>(`/myopportunity/action/${opportunityId}/save/remove`);
  return data;
};

export const isOpportunitySaved = async (
  opportunityId: string,
): Promise<Opportunity> => {
  const { data } = await (
    await ApiClient
  ).get<Opportunity>(`/myopportunity/action/${opportunityId}/saved`);
  return data;
};

export const performActionSendForVerificationManual = async (
  opportunityId: string,
  model: MyOpportunityRequestVerify,
): Promise<any> => {
  const formData = objectToFormData(model);

  await (
    await ApiClient
  ).put(`/myopportunity/action/${opportunityId}/verify`, formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });
};

export const getVerificationStatus = async (
  opportunityId: string,
  context?: GetServerSidePropsContext,
): Promise<MyOpportunityResponseVerify> => {
  const instance = context ? ApiServer(context) : await ApiClient;
  const { data } = await instance.post<MyOpportunityResponseVerify>(
    `/myopportunity/action/verify/status?opportunityId=${opportunityId}`,
  );

  return data;
};

export const searchMyOpportunities = async (
  filter: MyOpportunitySearchFilter,
  context?: GetServerSidePropsContext,
): Promise<MyOpportunitySearchResults> => {
  const instance = context ? ApiServer(context) : await ApiClient;

  const { data } = await instance.post<MyOpportunitySearchResults>(
    `/myopportunity/search`,
    filter,
  );
  return data;
};

export const searchMyOpportunitiesAdmin = async (
  filter: MyOpportunitySearchFilterAdmin,
  context?: GetServerSidePropsContext,
): Promise<MyOpportunitySearchResults> => {
  const instance = context ? ApiServer(context) : await ApiClient;

  const { data } = await instance.post<MyOpportunitySearchResults>(
    `/myopportunity/search/admin`,
    filter,
  );
  return data;
};

export const performActionVerifyBulk = async (
  model: MyOpportunityRequestVerifyFinalizeBatch,
  context?: GetServerSidePropsContext,
): Promise<MyOpportunityResponseVerifyFinalizeBatch> => {
  const instance = context ? ApiServer(context) : await ApiClient;

  const { data } = await instance.patch(
    `/myopportunity/verification/finalize/batch`,
    model,
  );

  return data;
};

export const performActionViewed = async (
  opportunityId: string,
  context?: GetServerSidePropsContext,
): Promise<any> => {
  const instance = context ? ApiServer(context) : await ApiClient;
  await instance.put(`/myopportunity/action/${opportunityId}/view`);
};

export const getOpportunitiesForVerification = async (
  organisations?: string[],
  verificationStatuses?: VerificationStatus[] | string[] | null,
  context?: GetServerSidePropsContext,
): Promise<MyOpportunitySearchCriteriaOpportunity[]> => {
  const instance = context ? ApiServer(context) : await ApiClient;

  let querystring = "";
  if (organisations) {
    querystring += `organizations=${organisations.join(",")}`;
  }
  if (verificationStatuses) {
    querystring += `&verificationStatuses=${verificationStatuses.join(",")}`;
  }
  if (querystring.length > 0) {
    querystring = `?${querystring}`;
  }

  const { data } = await instance.get<MyOpportunitySearchCriteriaOpportunity[]>(
    `/myopportunity/search/filter/opportunity${querystring}`,
  );
  return data;
};

export const performActionCancel = async (
  opportunityId: string,
  context?: GetServerSidePropsContext,
): Promise<any> => {
  const instance = context ? ApiServer(context) : await ApiClient;
  await instance.delete(`/myopportunity/action/${opportunityId}/verify/delete`);
};

export const performActionInstantVerificationManual = async (
  linkId: string,
  context?: GetServerSidePropsContext,
): Promise<void> => {
  const instance = context ? ApiServer(context) : await ApiClient;
  await instance.put(`/myopportunity/action/link/${linkId}/verify`);
};

export const getMyOpportunitiesExportToCSV = async (
  filter: MyOpportunitySearchFilterAdmin,

  context?: GetServerSidePropsContext | GetStaticPropsContext,
): Promise<File> => {
  const instance = context ? ApiServer(context) : await ApiClient;

  const { data } = await instance.post(
    `/myopportunity/search/admin/csv`,
    filter,
    {
      responseType: "blob", // set responseType to 'blob' or 'arraybuffer'
    },
  );

  // create the file name
  const date = new Date();
  const dateString = `${date.getFullYear()}-${
    date.getMonth() + 1
  }-${date.getDate()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  const fileName = `Verifications_${dateString}.csv`;

  // create a new Blob object using the data
  const blob = new Blob([data], { type: "text/csv" });

  // create a new File object from the Blob
  const file = new File([blob], fileName);

  return file;
};

export const performActionNavigateExternalLink = async (
  opportunityId: string,
  context?: GetServerSidePropsContext,
): Promise<any> => {
  const instance = context ? ApiServer(context) : await ApiClient;
  await instance.put(
    `/myopportunity/action/${opportunityId}/navigateExternalLink`,
  );
};
