import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DaemonsInfoComponent } from './daemons-info.component';

describe('DaemonsInfoComponent', () => {
  let component: DaemonsInfoComponent;
  let fixture: ComponentFixture<DaemonsInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaemonsInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaemonsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
