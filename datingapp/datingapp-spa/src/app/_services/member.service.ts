import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IMember } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MemberService {
baseUrl = environment.apiUrl;
members: IMember[] = [];
  constructor(private http: HttpClient) { }

  getMembers()  {
    if (this.members.length > 0) { return of(this.members); }
    return this.http.get<IMember[]>(this.baseUrl + 'users').pipe(
      map((members) => {
        this.members = members;
        return members;
      })
    );
  }

  getMember(username: string) {
    const member = this.members.find(m => m.username === username);
    if (member) { return of(member); }
    return this.http.get<IMember>(this.baseUrl +  'users/' + username);
  }

  updateMember(member: IMember) {
   return this.http.put(this.baseUrl + 'users', member).pipe(
     map(() => {
       const index = this.members.indexOf(member);
       this.members[index] =  member;
     })
   );
  }
}
