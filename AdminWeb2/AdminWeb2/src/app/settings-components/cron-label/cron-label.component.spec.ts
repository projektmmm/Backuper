import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CronLabelComponent } from './cron-label.component';

describe('CronLabelComponent', () => {
  let component: CronLabelComponent;
  let fixture: ComponentFixture<CronLabelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CronLabelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CronLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
