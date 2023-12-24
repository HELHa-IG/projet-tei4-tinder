import { Component, ElementRef, HostListener, ViewChild, ViewChildren, QueryList, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { jwtDecode } from 'jwt-decode';


interface JwtPayload {
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
  exp: number;
  iat: number;
}




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  @ViewChildren('label') label!: QueryList<ElementRef>;
  @ViewChildren('input') input!: QueryList<ElementRef>;
  @ViewChildren('responseNotification') responseNotification!: QueryList<ElementRef>;


  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(60)]),
    password: new FormControl('', [Validators.required, Validators.minLength(0), Validators.maxLength(30)]),
  });

  constructor(private router : Router,private userService: UserService, private cookieService: CookieService) {}

  onFocus(nombre: number) {
    this.label.forEach((element, index) => {
      if (index == nombre) {
        element.nativeElement.style.zIndex = '2';
        element.nativeElement.style.transform = 'translateY(-140%)';
        element.nativeElement.style.left = '60px';
        element.nativeElement.style.top = '30px';
        element.nativeElement.style.fontSize = '0.9em';
        element.nativeElement.style.color = '#000';
        this.input.toArray()[index].nativeElement.style.border = '2px solid #000';
      }
    });
  }

  onBlur(nombre: number): void {
    const inputElement = this.input.toArray()[nombre].nativeElement;
    if (inputElement.value === '') {
      this.label.forEach((element, index) => {
        if (index == nombre) {
          element.nativeElement.style.zIndex = '0';
          element.nativeElement.style.transform = 'none';
          element.nativeElement.style.left = '60px';
          element.nativeElement.style.fontSize = 'initial';
          element.nativeElement.style.color = 'rgba(0, 0, 0, 0.322)';
          inputElement.style.border = '2px solid rgba(0, 0, 0, 0.322)';
        }
      });
    }
  }

  login() {

    if (this.loginForm.valid && !this.cookieService.check('jwt')) {
    let user:User = new User(this.loginForm.value.username!, this.loginForm.value.password!,'');
    this.resetNotification();
    this.responseNotification.toArray()[0].nativeElement.style.display = 'flex';
    this.userService.login(user).subscribe(
      (jwt:any) => {
        let date = new Date();
        date.setTime(date.getTime() + (1 * 60 * 60 * 1000));
        const decoded = jwtDecode<JwtPayload>(jwt);
        let name = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
        console.log(decoded);
        this.cookieService.set('name', name, date);
        this.cookieService.set('token', jwt, date);
        this.userService.getByName(this.cookieService.get("name")).subscribe(user => {
          this.cookieService.set('id', user.id!.toString(), date);
        });
        this.resetNotification();
        this.responseNotification.toArray()[1].nativeElement.style.display = 'flex';
        this.waitResetNotification();
        setTimeout(() => {
          this.router.navigate(['/deck-list']);
        }, 500);

      },
      error => {
        console.error(error);
        console.log(error.status);
        this.resetNotification();
        this.responseNotification.toArray()[2].nativeElement.style.display = 'flex';
        this.waitResetNotification();
      }
    );
    }
    else{
      this.loginForm.markAllAsTouched();
    }
  }


  resetNotification() {
    this.responseNotification.forEach(element => {
      element.nativeElement.style.display = 'none';
    });
  }

  waitResetNotification() {
    setTimeout(() => {
      this.resetNotification();
    }, 10000);
  }




}
