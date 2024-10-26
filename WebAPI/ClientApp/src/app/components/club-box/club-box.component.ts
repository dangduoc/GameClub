import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IGameClub } from '../../models/game_club';
import { CommonModule } from '@angular/common';
import { SafeHtmlPipe } from '../../pipes/safe-html.pipe';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-club-box',
  standalone: true,
  imports: [
    CommonModule,
    SafeHtmlPipe,
    RouterLink
  ],
  templateUrl: './club-box.component.html',
  styleUrl: './club-box.component.scss'
})
export class ClubBoxComponent {
  @Input() data!: IGameClub;
  @Output() onClick = new EventEmitter();
}
