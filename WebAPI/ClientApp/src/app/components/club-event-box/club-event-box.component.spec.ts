import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubEventBoxComponent } from './club-event-box.component';

describe('ClubEventBoxComponent', () => {
  let component: ClubEventBoxComponent;
  let fixture: ComponentFixture<ClubEventBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClubEventBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClubEventBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
