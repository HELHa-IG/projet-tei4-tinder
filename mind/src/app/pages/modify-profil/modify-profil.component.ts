import { Component, ElementRef, HostListener, ViewChild,AfterViewInit,ViewChildren,QueryList, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { RouterLink, RouterLinkWithHref, Router, NavigationEnd } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { CookieService } from 'ngx-cookie-service';
import { UserFull } from 'src/app/models/user_full';

@Component({
  selector: 'app-modify-profil',
  templateUrl: './modify-profil.component.html',
  styleUrls: ['./modify-profil.component.scss']
})
export class ModifyProfilComponent {

  @ViewChildren('label') label!: QueryList<ElementRef>;
  @ViewChildren('input') input!: QueryList<ElementRef>;
  @ViewChildren('responseNotification') responseNotification!: QueryList<ElementRef>;

  profilId!: number;
  hashPassword!: string;
  saltPassword!: string;
  creationDate!: string;

  register = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(60)]),
    email: new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(60)]),
  });

  constructor(private router: Router,private userService: UserService, private cookieService:CookieService) { }

  ngAfterViewInit(){
    this.userService.getByName(this.cookieService.get("name")).subscribe(user => {
      this.register.setValue({
        username: user.username,
        email: user.email,
      });
      this.profilId = user.id!;
      this.hashPassword = user.passwordHash;
      this.saltPassword = user.passwordSalt;
      this.creationDate = user.creationDate;
    });

  }

  onFocus(nombre: number): void {
    this.label.forEach((element, index) => {
      if (index == nombre) {
        element.nativeElement.style.zIndex = '2';
        element.nativeElement.style.transform = 'translateY(-140%) translateX(15px)';
        element.nativeElement.style.fontSize = '0.9em';
        element.nativeElement.style.color = '#000';
        this.input.toArray()[index].nativeElement.style.border = '2px solid black';
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
          element.nativeElement.style.fontSize = 'initial';
          element.nativeElement.style.color = 'rgba(0, 0, 0, 0.322)';
          inputElement.style.border = '2px solid rgba(0, 0, 0, 0.322)';
        }
      });
    }
  }

  sendData(){
    if (this.register.valid) {
    this.resetNotification();
    this.responseNotification.toArray()[0].nativeElement.style.display = 'flex';

    let user:UserFull = new UserFull(this.register.value.username!,this.register.value.email!,this.creationDate,this.hashPassword,this.saltPassword,this.profilId);

    this.userService.updateUser(user).subscribe(
      response => {
        this.register.reset();
        this.resetNotification();
        this.responseNotification.toArray()[2].nativeElement.style.display = 'flex';
        this.cookieService.set('name', user.username);
        this.waitResetNotification();
        setTimeout(() => {
          this.router.navigate(['/deck-list']);
        }, 2000);
      },
      error => {
        console.log(error['error']);
        if(error['error'] == "Champs invalide"){

        }
        this.resetNotification();
        this.responseNotification.toArray()[1].nativeElement.style.display = 'flex';
        this.waitResetNotification();
      }
    );
    }
    else{
      this.register.markAllAsTouched();
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
    }, 2000);
  }

  passwordMatchValidator(control: AbstractControl) {
    const mdp = control.root.get('mdp');
    const confirmeMdp = control.value;

    if (mdp && mdp.value !== confirmeMdp) {
      return { passwordMismatch: true };
    } else {
      return null;
    }
  }


}
