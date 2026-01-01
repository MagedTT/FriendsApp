import { Component, inject, Input, signal } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterComponent } from "../account/register/register.component";
import { User } from '../../types/User';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  protected registerMode = signal<boolean>(false);

  showRegister(value: boolean) {
    this.registerMode.set(value);
  }
}
