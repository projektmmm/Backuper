import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DaemonsRequestComponent } from './daemons-request.component';

describe('DaemonsRequestComponent', () => {
  let component: DaemonsRequestComponent;
  let fixture: ComponentFixture<DaemonsRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaemonsRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaemonsRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
