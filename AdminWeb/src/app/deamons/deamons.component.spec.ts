import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeamonsComponent } from './deamons.component';

describe('DeamonsComponent', () => {
  let component: DeamonsComponent;
  let fixture: ComponentFixture<DeamonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeamonsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeamonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
