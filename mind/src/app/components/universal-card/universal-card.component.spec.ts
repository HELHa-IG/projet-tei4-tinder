import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversalCardComponent } from './universal-card.component';

describe('UniversalCardComponent', () => {
  let component: UniversalCardComponent;
  let fixture: ComponentFixture<UniversalCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UniversalCardComponent]
    });
    fixture = TestBed.createComponent(UniversalCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
