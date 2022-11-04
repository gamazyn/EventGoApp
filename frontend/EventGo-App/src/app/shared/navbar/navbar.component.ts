import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '@app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  isCollapsed = true;

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/user/login');
  }

  showMenu(): boolean {
    return this.router.url !== '/user/login';
  }
}
