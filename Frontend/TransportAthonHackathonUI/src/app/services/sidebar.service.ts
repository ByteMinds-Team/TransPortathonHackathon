import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, debounceTime, takeLast } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private readonly isSidebarOpen_ = new BehaviorSubject<boolean>(false);
  private readonly router = inject(Router);

  constructor() {
    this.router.events.pipe(debounceTime(500)).subscribe(() => this.isSidebarOpen_.next(false))
  }

  openSidebar() {
    this.isSidebarOpen_.next(true)
  }

  closeSidebar() {
    this.isSidebarOpen_.next(false)
  }

  get isSidebarOpen$() {
    return this.isSidebarOpen_.asObservable();
  }
}
