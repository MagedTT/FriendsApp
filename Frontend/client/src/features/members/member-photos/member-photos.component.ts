import { Component, inject, OnInit } from '@angular/core';
import { MemberService } from '../../../core/services/member.service';
import { ActivatedRoute } from '@angular/router';
import { Photo } from '../../../types/Photo';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-member-photos',
  imports: [AsyncPipe],
  templateUrl: './member-photos.component.html',
  styleUrl: './member-photos.component.css'
})
export class MemberPhotosComponent implements OnInit {
  private memberService = inject(MemberService);
  private route = inject(ActivatedRoute);
  protected photos$?: Observable<Photo[]>;

  ngOnInit(): void {
    this.photos$ = this.loadPhotos();
  }

  loadPhotos() {
    const id = this.route.parent?.snapshot.paramMap.get('id');

    if (!id)
      return;

    return this.memberService.getMemberPhotos(id);
  }

  get photoMocks() {
    return Array.from({ length: 20 }, (_, i) => ({
      url: '/user.png'
    }))
  }
}
