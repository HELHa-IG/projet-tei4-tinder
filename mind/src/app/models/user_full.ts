export class UserFull {
  id?: number;
  username: string;
  email: string;
  creationDate: string;
  passwordHash: string;
  passwordSalt: string;

  constructor(username: string, email: string, creationDate: string, passwordHash: string, passwordSalt: string, id?: number) {
    this.username = username;
    this.email = email;
    this.creationDate = creationDate;
    this.passwordHash = passwordHash;
    this.passwordSalt = passwordSalt;
    if (id) {
      this.id = id;
    }
  }
}
