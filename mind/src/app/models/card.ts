import { Deck } from './deck';

export class Card {
  id: number;
  frontText: string;
  backText: string;
  frontImg: string;
  backImg: string;
  creationDate: Date;
  lastModifiedDate: Date;
  deckId: number;

  constructor(id: number, frontText: string, backText: string, frontImg: string, backImg: string, creationDate: Date, lastModifiedDate: Date, deckId: number) {
      this.id = id;
      this.frontText = frontText;
      this.backText = backText;
      this.frontImg = frontImg;
      this.backImg = backImg;
      this.creationDate = creationDate;
      this.lastModifiedDate = lastModifiedDate;
      this.deckId = deckId;
  }
}
