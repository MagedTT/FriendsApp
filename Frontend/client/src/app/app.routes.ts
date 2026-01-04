import { Routes } from '@angular/router';
import { HomeComponent } from '../features/home/home.component';
import { MemberListComponent } from '../features/members/member-list/member-list.component';
import { MemberDetailedComponent } from '../features/members/member-detailed/member-detailed.component';
import { ListsComponent } from '../features/lists/lists.component';
import { MessagesComponent } from '../features/messages/messages.component';
import { authGuard } from '../core/guards/auth.guard';
import { MemberProfileComponent } from '../features/members/member-profile/member-profile.component';
import { MemberPhotosComponent } from '../features/members/member-photos/member-photos.component';
import { MemberMessagesComponent } from '../features/members/member-messages/member-messages.component';
import { memberResolver } from '../features/members/member.resolver';
import { preventUnsavedChangesGuard } from '../core/guards/prevent-unsaved-changes.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'members', component: MemberListComponent, canActivate: [authGuard] },
    {
        path: 'members/:id',
        resolve: { member: memberResolver },
        runGuardsAndResolvers: 'always',
        component: MemberDetailedComponent,
        canActivate: [authGuard],
        children: [
            { path: '', redirectTo: 'profile', pathMatch: 'full' },
            {
                path: 'profile',
                component: MemberProfileComponent,
                title: 'Profile',
                canDeactivate: [preventUnsavedChangesGuard]
            },
            { path: 'photos', component: MemberPhotosComponent, title: 'Photos' },
            { path: 'messages', component: MemberMessagesComponent, title: 'Messages' }
        ]
    },
    { path: 'lists', component: ListsComponent, canActivate: [authGuard] },
    { path: 'messages', component: MessagesComponent, canActivate: [authGuard] },
    { path: '**', component: HomeComponent },
];