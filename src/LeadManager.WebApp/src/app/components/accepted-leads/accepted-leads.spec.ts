import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptedLeads } from './accepted-leads';

describe('AcceptedLeads', () => {
  let component: AcceptedLeads;
  let fixture: ComponentFixture<AcceptedLeads>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AcceptedLeads]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AcceptedLeads);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
