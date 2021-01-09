import { IPhoto } from './photo';

export interface IMember {
  UserName: string;
  Gender: string;
  DateOfBirth: string;
  KnownAs: string;
  Created: Date;
  LastActive: Date;
  Introduction: string;
  LookingFor: string;
  Interests: string;
  City: string;
  Country: string;
  Photos: IPhoto[];
}


