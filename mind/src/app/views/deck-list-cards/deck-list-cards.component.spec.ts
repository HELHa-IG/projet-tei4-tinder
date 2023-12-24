import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckListCardsComponent } from './deck-list-cards.component';

describe('DeckListCardsComponent', () => {
  let component: DeckListCardsComponent;
  let fixture: ComponentFixture<DeckListCardsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeckListCardsComponent]
    });
    fixture = TestBed.createComponent(DeckListCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
