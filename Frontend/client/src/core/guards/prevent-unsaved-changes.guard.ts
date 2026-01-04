import { CanDeactivateFn } from '@angular/router';
import { MemberProfileComponent } from '../../features/members/member-profile/member-profile.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberProfileComponent> = (component) => {
  if (component.editForm?.dirty) {
    return confirm('Are your sure you want to leave?');
  }

  return true;
};
