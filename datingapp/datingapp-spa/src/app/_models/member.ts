import { IPhoto } from './photo';

export interface IMember {
  username: string;
  photoUrl: string;
  gender: string;
  age: number;
  knownAs: string;
  created: Date;
  lastActive: Date;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photos: IPhoto[];
}


