import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  isOpen: boolean = false;

  // Function to toggle the modal's visibility
  toggleModal(co : boolean) {
    this.isOpen = co; // Invert the current state
  }
}
