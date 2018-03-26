import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendSettingsComponent } from './send-settings.component';

describe('SendSettingsComponent', () => {
  let component: SendSettingsComponent;
  let fixture: ComponentFixture<SendSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
