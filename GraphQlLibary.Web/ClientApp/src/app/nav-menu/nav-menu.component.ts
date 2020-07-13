import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private router: Router) { }

  collapse() {
    this.isExpanded = false;
  }

  isAuthorize() {
    let token = sessionStorage.getItem('token');

    if (token == null || token == undefined || token == '') {
      return false;
    }

    return true;
  }

  logout() {
    sessionStorage.removeItem('token');
    this.router.navigate(['/']);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
