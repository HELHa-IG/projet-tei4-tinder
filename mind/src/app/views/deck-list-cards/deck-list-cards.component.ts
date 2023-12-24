import { Component, ElementRef, ViewChild } from '@angular/core';
import { DeckListCardsService } from '../../_services/deck-list-cards.service';
import { Deck } from 'src/app/models/deck';
import { User } from 'src/app/models/user';
import { Card } from 'src/app/models/card';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { CardUploadService } from 'src/app/_services/card-upload.service';


@Component({
  selector: 'app-deck-list-cards',
  templateUrl: './deck-list-cards.component.html',
  styleUrls: ['./deck-list-cards.component.css']
})
export class DeckListCardsComponent {
  deck!: Deck;
  cards!: Card[];
  user!: User;
  deckId!: number;
  modalOpen = false;
  editCardModalOpen = false;
  selectedCard!: Card;
  imageFront!: string;
  imageBack!: string;

  @ViewChild('frontPhoto') frontPhoto!: ElementRef;
  @ViewChild('backPhoto') backPhoto!: ElementRef;

  constructor(private deckListCardsService: DeckListCardsService, private router: Router, private route: ActivatedRoute, private cardUploadService: CardUploadService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.deckId = Number(params.get('id'))
    });
    this.deckListCardsService.getCards(this.deckId).subscribe(cards => {
      this.cards = cards;
    });
  }

  flipped = false;

  flipCard() {
    this.flipped = !this.flipped;
  }

  openModal(): void {
    this.modalOpen = true;
  }

  closeModal(): void {
    this.modalOpen = false;
  }

  openEditModal(card: Card): void {
    this.selectedCard = card;
    this.editCardModalOpen = true;
  }

  closeEditModal(): void {
    this.editCardModalOpen = false;
  }

  editCard(formValue: any): void {
    const frontPhotoFile = this.frontPhoto.nativeElement.files[0];
    const backPhotoFile = this.backPhoto.nativeElement.files[0];
    var card = new Card(
      this.selectedCard.id,
      formValue.frontText,
      formValue.backText,
      this.selectedCard.frontImg,
      this.selectedCard.backImg,
      this.selectedCard.creationDate,
      new Date(),
      this.selectedCard.deckId,
    );

    var formData = new FormData();
    formData.append('FrontImg', frontPhotoFile);
    formData.append('FrontImgName', card.frontImg);
    formData.append('BackImg', backPhotoFile);
    formData.append('BackImgName', card.backImg);
    formData.append('CardId', card.id.toString());


    console.log(card);
    this.deckListCardsService.updateCard(card.id, card).subscribe(updatedCard => {
      this.cardUploadService.uploadCard(formData).subscribe((value) => {
        card.frontImg = value.frontImgName;
        card.backImg = value.backImgName;
      });
      this.closeEditModal();

    });

    // Supprimez l'ancienne carte du tableau
    this.cards = this.cards.filter(c => c.id !== card.id);

    // Ajoutez la nouvelle carte au tableau
    this.cards.push(card);
    console.log(this.cards);

    this.closeEditModal();

}


  createCard(formValue: any): void {
    if(formValue.frontText != "" && formValue.backText != "") {
      var card = new Card(
        0,
        formValue.frontText,
        formValue.backText,
        "",
        "",
        new Date(),
        new Date(),
        this.deckId,
      );
      this.deckListCardsService.createCard(card).subscribe(card => {
        this.cards.push(card);
        this.closeModal();
      });
    }
  }

  deleteCard(card: Card): void {
    this.deckListCardsService.deleteCard(card.id).subscribe(() => {
      this.cards = this.cards.filter(c => c.id !== card.id);
    });
  }


  GoToJouer(): void {
    if(this.cards.length != 0)
      this.router.navigate(['/game', this.deckId]);
  }
}

