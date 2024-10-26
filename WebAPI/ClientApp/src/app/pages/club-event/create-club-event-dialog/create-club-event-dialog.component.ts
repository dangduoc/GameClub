import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalTitleDirective, NzModalFooterDirective, NzModalRef, NZ_MODAL_DATA } from 'ng-zorro-antd/modal';
import { BaseFormComponent } from '../../../components/base-form';
import { GameClubService } from '../../../services/club.service';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { IGameClub } from '../../../models/game_club';

@Component({
  selector: 'app-create-club-event-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzFormModule,
    NzInputModule,
    NzDatePickerModule,
    NzButtonModule,
    NzModalTitleDirective,
    NzModalFooterDirective,
  ],
  templateUrl: './create-club-event-dialog.component.html',
  styleUrl: './create-club-event-dialog.component.scss',
  providers: [
    NzMessageService
  ]
})
export class CreateClubEventDialogComponent extends BaseFormComponent implements OnInit {



  club: IGameClub;
  constructor(
    private ref: NzModalRef,
    private service: GameClubService,
    private messageService: NzMessageService,
    @Inject(NZ_MODAL_DATA) modalData: any,
  ) {
    super({
      clubId: [null, [Validators.required]],
      title: [null, [Validators.required]],
      scheduledAt: [null, [Validators.required]],
      description: [null],
    });
    this.club = modalData.club;
    this.f['clubId'].setValue(this.club.id);
  }
  ngOnInit(): void {

  }

  override save(): void | Promise<void> {
    this.saving = true;
    this.subs.sink = this.service.createClubEvent(this.club.id, this.formModel)
      .subscribe({
        next: (data) => {
          this.messageService.success("One club event has added successfully!");
          this.ref.close(data);
        },
        error: (e) => {
          this.saving = false;
          switch (e.status) {
            case 404:
              this.messageService.error("Error: 404");
              break;
            case 409:
              this.messageService.error("Error: 409");
              break;
            case 400:
              this.messageService.error("Error: 400");
              break;
            case 500:
              this.messageService.error("Error: 500");
              break;
            default:
              this.messageService.error("Unknow Error");
              break;
          }
        },
        complete: () => {
          this.saving = false;
        }
      });
  }

  close() {
    this.ref.close();
  }
}
