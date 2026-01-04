import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { filter, Observable } from 'rxjs';
import { Member } from '../../../types/Members';
import { AccountService } from '../../../core/account.service';
import { MemberService } from '../../../core/services/member.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-member-detailed',
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './member-detailed.component.html',
  styleUrl: './member-detailed.component.css'
})
export class MemberDetailedComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private accountService = inject(AccountService);
  protected memberService = inject(MemberService);
  private router = inject(Router);
  // protected member$?: Observable<Member>;
  // protected member = signal<Member | undefined>(undefined);
  protected title = signal<string | undefined>('Profile');
  protected isCurrentUser = computed(() => {
    return this.accountService.currentUser()?.id === this.route.snapshot.paramMap.get('id');
  });

  ngOnInit(): void {
    // this.member$ = this.loadMember();
    // this.route.data.subscribe({
    //   next: data => this.member.set(data['member'])
    // });

    this.title.set(this.route.firstChild?.snapshot?.title);

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe({
      next: () => {
        this.title.set(this.route.firstChild?.snapshot?.title);
      }
    });
  }

  loadMember() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id)
      return;
    return this.memberService.getMember(id);
  }
}