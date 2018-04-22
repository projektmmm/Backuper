import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DaemonAdderComponent } from './daemon-adder.component';

describe('DaemonAdderComponent', () => {
  let component: DaemonAdderComponent;
  let fixture: ComponentFixture<DaemonAdderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaemonAdderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaemonAdderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
