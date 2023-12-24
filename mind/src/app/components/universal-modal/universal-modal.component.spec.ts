import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversalModalComponent } from './universal-modal.component';

describe('UniversalModalComponent', () => {
  let component: UniversalModalComponent;
  let fixture: ComponentFixture<UniversalModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UniversalModalComponent]
    });
    fixture = TestBed.createComponent(UniversalModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
