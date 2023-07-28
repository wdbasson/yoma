export interface User {
  id: string;
  emailConfirmed: boolean;
  displayName: string | null;
  phoneNumber: string | null;
  countryId: string | null;
  countryCodeAlpha2: string | null;
  countryOfResidenceId: string | null;
  countryOfResidenceCodeAlpha2: string | null;
  photoId: string | null;
  genderId: string | null;
  dateOfBirth: string | null;
  dateLastLogin: string | null;
  externalId: string | null;
  zltoWalletId: string | null;
  zltoWalletCountryId: string | null;
  zltoWalletCountryCodeAlpha2: string | null;
  tenantId: string | null;
  dateCreated: string;
  dateModified: string;
}
