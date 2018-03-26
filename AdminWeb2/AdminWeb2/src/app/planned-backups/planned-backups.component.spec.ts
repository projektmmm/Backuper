import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlannedBackupsComponent } from './planned-backups.component';

describe('PlannedBackupsComponent', () => {
  let component: PlannedBackupsComponent;
  let fixture: ComponentFixture<PlannedBackupsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlannedBackupsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlannedBackupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
