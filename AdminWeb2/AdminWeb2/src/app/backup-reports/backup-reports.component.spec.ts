import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BackupReportsComponent } from './backup-reports.component';

describe('BackupReportsComponent', () => {
  let component: BackupReportsComponent;
  let fixture: ComponentFixture<BackupReportsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BackupReportsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BackupReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
