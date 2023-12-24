import {Component, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {Card} from "../../models/card";
import {DeckListCardsService} from "../../_services/deck-list-cards.service";

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit{
  indexActuel = 0;
  flipped = false;
  inputValue = '';
  reponsesCorrectes = 0;
  showResult = false;
  cards: Card[] = [];
  questionNumber = 1;

  deckId!: number;

  constructor(private deckListCardsService: DeckListCardsService, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.deckId = Number(params.get('id'))
    });
    this.deckListCardsService.getCards(this.deckId).subscribe(cards => {
      this.cards = cards;
    });
  }


  flipCard() {
    this.flipped = !this.flipped;
  }

  afficherElementSuivant() {
    this.questionNumber++;

    if (this.inputValue.toLowerCase() === this.cards[this.indexActuel].backText.toLowerCase()) {
      this.reponsesCorrectes++;
    }

    if (this.indexActuel < this.cards.length - 1) {
      this.indexActuel++;
    } else {
      this.showResult = true;
      return;
    }

    this.inputValue = '';
    this.flipped = false;
  }



  rejouer() {
    this.indexActuel = 0;
    this.questionNumber = 1;
    this.reponsesCorrectes = 0;
    this.showResult = false;
  }
}
