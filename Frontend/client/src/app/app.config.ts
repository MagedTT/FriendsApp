import { ApplicationConfig, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { InitService } from '../core/services/init.service';
import { jwtInterceptor } from '../core/interceptors/jwt.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withViewTransitions()),
    provideHttpClient(withInterceptors([jwtInterceptor])),
    provideAppInitializer(async () => {
      const initService = inject(InitService);

      try {
        return lastValueFrom(initService.init());
      } finally {
        const splash = document.getElementById('initial-splash');

        if (splash) {
          splash.remove();
        }
      }
    })
  ]
};
