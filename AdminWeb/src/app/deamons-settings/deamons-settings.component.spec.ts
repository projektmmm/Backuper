import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeamonsSettingsComponent } from './deamons-settings.component';

describe('DeamonsSettingsComponent', () => {
  let component: DeamonsSettingsComponent;
  let fixture: ComponentFixture<DeamonsSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeamonsSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeamonsSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
