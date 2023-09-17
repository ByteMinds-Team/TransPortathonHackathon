import {AsyncPipe, NgIf} from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import {Component, inject} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Router, RouterModule} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {SidebarComponent} from 'src/app/components/sidebar/sidebar.component';
import {AuthService} from 'src/app/services/auth.service';
import {SidebarService} from 'src/app/services/sidebar.service';

@Component({
  selector: 'app-home-layout',
  templateUrl: './home-layout.component.html',
  styleUrls: ['./home-layout.component.scss'],
  standalone: true,
  imports: [
    NgIf,
    AsyncPipe,
    RouterModule,
    SidebarComponent,
    FormsModule,
  ],
})
export class HomeLayoutComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly sidebarService = inject(SidebarService)
  private readonly toastr = inject(ToastrService)

  user$ = this.authService.currentUser$;
  isHoverUserInfo = false;
  searchText: string = "";

  async searchWord(word: string) {
    if (word === null || word.length === 0 || word.trim() === "")
      this.toastr.warning("You must enter text to search", "Warning")
    else
      await this.navigateToSearchResultPage(word);
  }

  openSidebar() {
    this.sidebarService.openSidebar();
  }

  closeSidebar() {
    this.sidebarService.closeSidebar();
  }

  navigateToSearchResultPage(word: string) {
    
  }
}
