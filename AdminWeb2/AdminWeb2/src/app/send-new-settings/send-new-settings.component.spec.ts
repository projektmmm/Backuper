import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendNewSettingsComponent } from './send-new-settings.component';

describe('DaemonsComponent', () => {
  let component: SendNewSettingsComponent;
  let fixture: ComponentFixture<SendNewSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendNewSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendNewSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
