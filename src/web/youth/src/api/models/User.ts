export interface User {
  id: string | null;
  email: string;
  emailConfirmed: boolean;
  firstName: string;
  surname: string;
  displayName: string | null;
  phoneNumber: string | null;
  countryId: string | null;
  countryOfResidenceId: string | null;
  genderId: string | null;
  dateOfBirth: string | null;
  photoId: string | null;
  photoURL: string | null;
  dateLastLogin: string | null;
  externalId: string | null;
  zltoWalletId: string | null;
  zltoWalletCountryId: string | null;
  zltoWalletCountryCodeAlpha2: string | null;
  tenantId: string | null;
  dateCreated: string;
  dateModified: string;
}

export interface UserProfileRequest {
  email: string;
  firstName: string;
  surname: string;
  displayName: string | null;
  phoneNumber: string | null;
  countryId: string | null;
  countryOfResidenceId: string | null;
  genderId: string | null;
  dateOfBirth: string | null;
  resetPassword: boolean;
}
