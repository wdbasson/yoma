export interface SSISchemaEntity {
  id: string;
  name: string;
  properties: SSISchemaEntityProperty[] | null;
}

export interface SSISchemaEntityProperty {
  id: string;
  attributeName: string;
  typeName: string;
  valueDescription: string;
  required: boolean;
}

export interface SSISchemaRequest {
  name: string;
  typeId: string;
  artifactType: ArtifactType | string | null;
  attributes: string[];
}

export enum ArtifactType {
  Indy,
  Ld_proof,
}

export enum SchemaType {
  Opportunity,
  YoID,
}

export interface SSISchemaType {
  id: string;
  name: string;
  description: string;
  supportMultiple: boolean;
}

export interface SSISchema {
  id: string;
  name: string;
  displayName: string;
  typeId: string;
  type: SchemaType;
  typeDescription: string;
  version: string;
  artifactType: ArtifactType;
  entities: SSISchemaEntity[] | null;
  propertyCount: number | null;
}