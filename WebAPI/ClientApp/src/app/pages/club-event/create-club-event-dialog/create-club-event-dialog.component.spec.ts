import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateClubEventDialogComponent } from './create-club-event-dialog.component';

describe('CreateClubEventDialogComponent', () => {
  let component: CreateClubEventDialogComponent;
  let fixture: ComponentFixture<CreateClubEventDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateClubEventDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateClubEventDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
