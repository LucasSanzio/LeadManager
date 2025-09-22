import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitedLeads } from './invited-leads';

describe('InvitedLeads', () => {
  let component: InvitedLeads;
  let fixture: ComponentFixture<InvitedLeads>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InvitedLeads]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvitedLeads);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
