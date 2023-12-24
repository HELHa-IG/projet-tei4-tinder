export class Historique {
  id: number;
  date: Date;
  size: number;
  correctAmount: number;
  userId: number;
  deckId: number;
  deck: {
    id: number;
    title: string;
    creationDate: Date;
    lastModifiedDate: Date;
    userID: number;
    user: {
      id: number;
      username: string;
      email: string;
      passwordHash: string;
      passwordSalt: string;
      creationDate: Date;
    };
  };

  constructor(
    id: number,
    date: Date,
    size: number,
    correctAmount: number,
    userId: number,
    deckId: number,
    deck: {
      id: number;
      title: string;
      creationDate: Date;
      lastModifiedDate: Date;
      userID: number;
      user: {
        id: number;
        username: string;
        email: string;
        passwordHash: string;
        passwordSalt: string;
        creationDate: Date;
      };
    } = {
      id: 0,
      title: '',
      creationDate: new Date(),
      lastModifiedDate: new Date(),
      userID: 0,
      user: {
        id: 0,
        username: '',
        email: '',
        passwordHash: '',
        passwordSalt: '',
        creationDate: new Date(),
      },
    }
  ) {
    this.id = id;
    this.date = date;
    this.size = size;
    this.correctAmount = correctAmount;
    this.userId = userId;
    this.deckId = deckId;
    this.deck = deck;
  }
}
