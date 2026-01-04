import { Component, HostListener, inject, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../../types/Members';
import { DatePipe } from '@angular/common';
import { MemberService } from '../../../core/services/member.service';
import { FormsModule, NgForm } from '@angular/forms';
import { EditableMember } from '../../../types/EditableMember';
import { ToastService } from '../../../core/services/toast.service';
import { AccountService } from '../../../core/account.service';

@Component({
  selector: 'app-member-profile',
  imports: [DatePipe, FormsModule],
  templateUrl: './member-profile.component.html',
  styleUrl: './member-profile.component.css'
})
export class MemberProfileComponent implements OnInit, OnDestroy {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: BeforeUnloadEvent) {
    if (this.editForm?.dirty) {
      $event.preventDefault();
    }
  }

  // private route = inject(ActivatedRoute);
  private toast = inject(ToastService);
  private accountService = inject(AccountService);
  protected memberService = inject(MemberService);
  // protected member = signal<Member | undefined>(undefined);
  protected editableMember: EditableMember = {
    name: '',
    description: '',
    city: '',
    country: ''
  };

  ngOnInit(): void {
    // this.route.parent?.data.subscribe({
    //   next: data => this.member.set(data['member'])
    // });

    this.editableMember = {
      name: this.memberService.member()?.name || '',
      description: this.memberService.member()?.description || '',
      city: this.memberService.member()?.city || '',
      country: this.memberService.member()?.country || '',
    }
  }

  updateProfile() {
    if (!this.memberService.member()) {
      return;
    }

    const updatedMember = { ...this.memberService.member(), ...this.editableMember };
    this.memberService.updateMember(this.editableMember).subscribe({
      next: () => {
        const currentUser = this.accountService.currentUser();

        if (currentUser && currentUser.name !== updatedMember.name) {
          currentUser.name = updatedMember.name;

          this.accountService.currentUser.set(currentUser);
        }
        this.toast.success('Profile updated successfully');
        this.memberService.editMode.set(false);
        this.memberService.member.set(updatedMember as Member);
        this.editForm?.reset(updatedMember);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.memberService.editMode()) {
      this.memberService.editMode.set(false);
    }
  }
}
