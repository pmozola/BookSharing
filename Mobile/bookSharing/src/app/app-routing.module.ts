import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { FirstTimeGuard } from './guards/first-time/first-time.guard';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./home/home.module').then(m => m.HomePageModule),
    canActivate: [FirstTimeGuard]
  },
  {
    path: 'add-book',
    loadChildren: () => import('./add-book/add-book.module').then(m => m.AddBookPageModule)
  },
  {
    path: 'intro',
    loadChildren: () => import('./intro-slider/intro-slider.module').then(m => m.IntroSliderPageModule)
  },
  {
    path: 'library',
    loadChildren: () => import('./library/library.module').then(m => m.LibraryPageModule)
  },
  {
    path: 'chat',
    loadChildren: () => import('./chat/chat.module').then(m => m.ChatPageModule)
  },
  {
    path: 'archive',
    loadChildren: () => import('./archive/archive.module').then(m => m.ArchivePageModule)
  },
  {
    path: 'user-profil',
    loadChildren: () => import('./user-profil/user-profil.module').then(m => m.UserProfilPageModule)
  },
  {
    path: 'wanted-books',
    loadChildren: () => import('./wanted-books/wanted-books.module').then(m => m.WantedBooksPageModule)
  },
  {
    path: "",
    redirectTo: "/home",
    pathMatch: "full"
  },
  {
    path: 'bookAdded',
    loadChildren: () => import('./book-added-successfully/book-added-successfully.module').then( m => m.BookAddedSuccessfullyPageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
