import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-universal-modal',
  templateUrl: './universal-modal.component.html',
  styleUrls: ['./universal-modal.component.css']
})
export class UniversalModalComponent {
  @Input() title: string = '';
  @Input() isOpen: boolean = false;
  @Input() content: string = '';
  @Output() submitEvent = new EventEmitter<String>();
  @Output() closeEvent = new EventEmitter<void>();
  formValue: any = '';


  submit(): void {
  this.submitEvent.emit(this.content);
}

  close(): void {
    this.closeEvent.emit();
  }
}
