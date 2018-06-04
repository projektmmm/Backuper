import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendNewSettingsOnetimeComponent } from './send-new-settings-onetime.component';

describe('SendNewSettingsOnetimeComponent', () => {
  let component: SendNewSettingsOnetimeComponent;
  let fixture: ComponentFixture<SendNewSettingsOnetimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendNewSettingsOnetimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendNewSettingsOnetimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
