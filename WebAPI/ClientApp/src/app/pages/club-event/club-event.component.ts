import { Component, OnDestroy, OnInit } from '@angular/core';
import { SubSink } from 'subsink';
import { IGameClub } from '../../models/game_club';
import { GameClubEventArgument, IGameClubEvent } from '../../models/game_club_event';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { GameClubService } from '../../services/club.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CreateClubEventDialogComponent } from './create-club-event-dialog/create-club-event-dialog.component';
import { CommonModule } from '@angular/common';
import { ClubEventBoxComponent } from '../../components/club-event-box/club-event-box.component';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-club-event',
  standalone: true,
  imports: [
    CommonModule,
    NzInputModule,
    NzIconModule,
    NzButtonModule,
    NzDividerModule,
    RouterLink,
    ClubEventBoxComponent,
  ],
  templateUrl: './club-event.component.html',
  styleUrl: './club-event.component.scss',
  providers: [
    NzModalService
  ]
})
export class ClubEventComponent implements OnInit, OnDestroy {
  private sub: SubSink = new SubSink();
  clubId: string = "";
  club?: IGameClub;
  events: IGameClubEvent[] = [];
  args: GameClubEventArgument = new GameClubEventArgument();
  totalItems: number = 0;
  constructor(
    private service: GameClubService,
    private modalService: NzModalService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private messageService: NzMessageService,
  ) {

  }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.clubId = params['id'];
        this.fetchClub();
      }
    })

  };
  //clubLoading: boolean = false;
  fetchClub() {
    // this.clubLoading = true;
    this.sub.sink = this.service.getClubById(this.clubId).subscribe({
      next: (data) => {
        this.club = data;
      },
      error: (e) => {
        // switch (e.status) {
        //   case 404:
        //     this.messageService.error("Error: 404");
        //     break;
        //   case 400:
        //     this.messageService.error("Error: 400");
        //     break;
        //   default:
        //     this.messageService.error("Unknow Error");
        //     break;
        // }
        this.router.navigate(['/']);
      },
      complete: () => {
        if (this.club) {
          this.fetchData();
        }

      }
    })
  }
  loading: boolean = false;
  fetchData() {
    this.loading = true;
    this.sub.sink = this.service.getClubEvents(this.clubId, this.args).subscribe({
      next: (res) => {
        this.events = [...this.events, ...(res.items ?? [])];
        this.totalItems = res.total ?? 0;
      },
      error: (e) => {
        this.messageService.warning('Something went wrong when loading your list');
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  loadMore() {
    if (this.totalItems > this.events.length) {
      this.args.start += 1;
    }
    this.fetchData();
  }

  openCreateDialog() {
    const modal = this.modalService.create({
      nzContent: CreateClubEventDialogComponent,
      nzWidth: 550,
      nzFooter: null,
      nzCentered: true,
      nzData: {
        club: this.club,
      },
      //  nzClassName: 'header-no-padding',
    });
    this.sub.sink = modal.afterClose.subscribe(v => {
      if (v) {
        this.events = [...this.events, v];
        this.totalItems += 1;
      }
    });
  }


}
