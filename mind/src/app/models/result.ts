export class Result {
  date: Date;
  size: number;
  correctAmount: number;
  userID: number;
  deckId: number;

  constructor(date: Date, size: number, correctAmount: number, userID: number, deckId: number) {
    this.date = date;
    this.size = size;
    this.correctAmount = correctAmount;
    this.userID = userID;
    this.deckId = deckId;
  }
}
