import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SidebarService } from 'src/app/services/sidebar.service';
import { RolesDto } from 'src/app/models/roles.dto';
import { Role } from 'src/app/models/role';

const ICONS = {
  words: "../assets/ic_fluent_notepad_24_filled.svg",
  quizes: "../assets/ic_fluent_note_edit_24_filled.svg",
  addWord: "../assets/ic_fluent_add_24_filled.svg"
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  imports: [
    CommonModule,
    RouterModule,
  ],
})
export class SidebarComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly sidebarService = inject(SidebarService);
  isSidebarOpen$ = this.sidebarService.isSidebarOpen$;

  filters: Feature[] = [
    { name: "Taşıma İstekleri", route: "offers", icon: null, role: null },
    { name: "Tekliflerim", route: "myoffers", icon: null, role: null },
    { name: "Randevularım", route: "appointments", icon: null, role: null },
    { name: "Mesajlarım", route: "messages", icon: null, role: null },
    { name: "Araçlarım", route: "vehicles", icon: null, role: 'corporate' },
    { name: "Ekiplerim", route: "crews", icon: null, role: 'corporate' },
  ]

  selectedFilterOption: Feature = this.filters[0];

  async logout() {
    this.authService.logout();
    await this.router.navigateByUrl('login');
  }

  openSidebar() {
    this.sidebarService.openSidebar();
  }

  closeSidebar() {
    this.sidebarService.closeSidebar();
  }

  isInRole(role: Role): boolean {
    return this.authService.isInRole(role)
  }
}

class Feature {
  name: string
  route: string
  icon: string | null
  role: Role
}