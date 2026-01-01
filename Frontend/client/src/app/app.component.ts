import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { NavComponent } from '../layout/nav/nav.component';
import { AccountService } from '../core/account.service';
import { HomeComponent } from '../features/home/home.component';
import { User } from '../types/User';

@Component({
  selector: 'app-root',
  imports: [NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected members = signal<User[]>([]);

  title = 'Client';

  async ngOnInit(): Promise<void> {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');

    if (!userString)
      return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers() {
    try {
      return lastValueFrom(this.http.get<User[]>('https://localhost:7208/api/Members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}

