import { Component, OnInit, Input } from '@angular/core';
import { IMember } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
@Input()   member: IMember;

}
