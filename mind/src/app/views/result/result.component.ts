import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {ResultService} from "../../_services/result.service";
import {Result} from "../../models/result";
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.css']
})
export class ResultComponent {
  @Input() score!: number ;
  @Input() nombreQuestion!: number;
  @Input() DeckId!: number;
  @Output() buttonPressed = new EventEmitter<boolean>();
  userId!: number;
  constructor(private router: Router, private ResultService: ResultService,  private cookieService: CookieService ) { }

  ngOnInit(): void {
    this.userId = Number(this.cookieService.get('id'));
    this.insertScort();
  }
  navigateTo(route: string) {
    this.router.navigate([route]).then(r => console.log(r));
  }

  buttonClicked() {
    this.buttonPressed.emit(true);
  }

  insertScort() {
    var result = new Result(
      new Date(),
      this.nombreQuestion,
      this.score,
      this.userId,
      this.DeckId
    );

    this.ResultService.insertResult(result).subscribe(result => {
      console.log(result);
    });
  }

  goToHistorique() {
    this.navigateTo('/historique');
  }
}
