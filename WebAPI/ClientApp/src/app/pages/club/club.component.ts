import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameClubArgument, IGameClub } from '../../models/game_club';
import { GameClubService } from '../../services/club.service';
import { NzModalService } from 'ng-zorro-antd/modal';
import { CreateClubDialogComponent } from './create-club-dialog/create-club-dialog.component';
import { CommonModule } from '@angular/common';
import { ClubBoxComponent } from '../../components/club-box/club-box.component';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { SubSink } from 'subsink';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { ClubEventComponent } from '../club-event/club-event.component';
import { RouterLink } from '@angular/router';
import { InputSearchDirective } from '../../directives/input-search.directive';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-club',
  standalone: true,
  imports: [
    CommonModule,
    NzIconModule,
    NzButtonModule,
    NzInputModule,
    NzPaginationModule,
    ClubBoxComponent,
    RouterLink,
    InputSearchDirective

  ],
  templateUrl: './club.component.html',
  styleUrl: './club.component.scss',
  providers: [
    NzModalService,
  ]
})
export class ClubComponent implements OnInit, OnDestroy {
  /**
   *
   */
  totalItems: number = 0;
  private sub: SubSink = new SubSink();
  args: GameClubArgument = new GameClubArgument();
  constructor(
    private service: GameClubService,
    private modalService: NzModalService,
    private messageService: NzMessageService,
  ) {

  }
  items: IGameClub[] = [];
  ngOnDestroy(): void {

  }
  ngOnInit(): void {
    this.fetchData();
  }
  loading: boolean = false;
  fetchData() {
    this.loading = true;
    this.sub.sink = this.service.getClubs(this.args).subscribe({
      next: (res) => {
        this.items = [...this.items, ...(res.items ?? [])];
        this.totalItems = res.total ?? 0;
      },
      error: (e) => {
        this.messageService.warning('Something went wrong when loading your list');
      },
      complete: () => {
        this.loading = false;
      }
    })
  }

  loadMore() {
    if (this.totalItems > this.items.length) {
      this.args.start += 1;
    }
    this.fetchData();
  }


  openCreateDialog() {
    const modal = this.modalService.create({
      nzContent: CreateClubDialogComponent,
      nzWidth: 550,
      nzFooter: null,
      nzCentered: true,
      nzData: {
      },
      //  nzClassName: 'header-no-padding',
    });
    this.sub.sink = modal.afterClose.subscribe(v => {
      if (v) {
        this.items.push(v);
      }
    });
  }

  onSearchTextChange($event: any) {
    this.args.searchBy = "name";
    this.args.searchText = $event ?? null;
    this.args.start = 0;
    this.items = [];
    this.fetchData();
  }



}
