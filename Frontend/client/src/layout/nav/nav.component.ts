import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountService } from '../../core/account.service';

@Component({
  selector: 'app-nav',
  imports: [CommonModule, FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  protected accountService = inject(AccountService);

  protected credentials: any = {};

  login() {
    this.accountService.login(this.credentials).subscribe({
      next: response => {
        console.log(response);
        this.credentials = {};
      },
      error: error => alert(error.message)
    });
  }

  logout() {
    this.accountService.logout();
  }
}
