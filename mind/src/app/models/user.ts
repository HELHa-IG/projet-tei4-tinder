export class User {
  id?: number;
  username: string;
  password: string;
  email: string;

  constructor(username: string, password: string, email: string, id?: number) {
    this.username = username;
    this.password = password;
    this.email = email;
    if (id) {
      this.id = id;
    }
  }
}

