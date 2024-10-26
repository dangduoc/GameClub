import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        loadComponent: () => import('./pages/club/club.component').then(x => x.ClubComponent),
    },
    {
        path: ':id/events',
        pathMatch: 'full',
        loadComponent: () => import('./pages/club-event/club-event.component').then(x => x.ClubEventComponent),
    }
];
