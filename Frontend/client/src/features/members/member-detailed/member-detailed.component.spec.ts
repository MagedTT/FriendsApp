import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberDetailedComponent } from './member-detailed.component';

describe('MemberDetailedComponent', () => {
  let component: MemberDetailedComponent;
  let fixture: ComponentFixture<MemberDetailedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MemberDetailedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberDetailedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
