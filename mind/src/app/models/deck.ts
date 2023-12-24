import { User } from './user';

export class Deck {
  id: number;
  title: string;
  creationDate: Date;
  lastModifiedDate: Date;
  user: User;
  userID: number;

  constructor(id: number, title: string, creationDate: Date, lastModifiedDate: Date, user: User, userID: number) {
      this.id = id;
      this.title = title;
      this.creationDate = creationDate;
      this.lastModifiedDate = lastModifiedDate;
      this.user = user;
      this.userID = userID;
  }
}
