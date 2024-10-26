import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';
import { IGameClubEvent } from '../../models/game_club_event';

@Component({
  selector: 'app-club-event-box',
  standalone: true,
  imports: [
    CommonModule,
    SafeHtmlPipe,
  ],
  templateUrl: './club-event-box.component.html',
  styleUrl: './club-event-box.component.scss'
})
export class ClubEventBoxComponent {
  @Input() data!: IGameClubEvent;
}
