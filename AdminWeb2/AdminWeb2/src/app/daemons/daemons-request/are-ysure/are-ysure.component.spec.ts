import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AreYsureComponent } from './are-ysure.component';

describe('AreYsureComponent', () => {
  let component: AreYsureComponent;
  let fixture: ComponentFixture<AreYsureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AreYsureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AreYsureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
