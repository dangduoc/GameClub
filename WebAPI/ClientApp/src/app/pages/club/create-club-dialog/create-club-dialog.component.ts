import { Component, OnInit } from '@angular/core';
import { NzModalFooterDirective, NzModalRef, NzModalTitleDirective } from 'ng-zorro-antd/modal';
import { BaseFormComponent } from '../../../components/base-form';
import { FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { GameClubService } from '../../../services/club.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';

@Component({
  selector: 'app-create-club-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NzFormModule,
    NzInputModule,
    NzButtonModule,
    NzModalTitleDirective,
    NzModalFooterDirective,
  ],
  templateUrl: './create-club-dialog.component.html',
  styleUrl: './create-club-dialog.component.scss',
  providers: [
    NzMessageService
  ]
})
export class CreateClubDialogComponent extends BaseFormComponent implements OnInit {




  constructor(
    private ref: NzModalRef,
    private service: GameClubService,
    private messageService: NzMessageService,
  ) {
    super({
      name: [null, [Validators.required]],
      description: [null],
    });
  }
  ngOnInit(): void {

  }

  override save(): void | Promise<void> {
    this.saving = true;
    this.subs.sink = this.service.createClub(this.formModel)
      .subscribe({
        next: (data) => {
          this.messageService.success("Your club has created successfully!");
          this.ref.close(data);
        },
        error: (e) => {
          this.saving = false;
          switch (e.status) {
            case 404:
              this.messageService.error("Error: 404");
              break;
            case 400:
              this.messageService.error("Error: 400");
              break;
            case 409:
              this.messageService.error("The club name already existed. Please try another one.");
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
