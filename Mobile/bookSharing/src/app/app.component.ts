import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Dom', url: '/home', icon: 'home' },
    { title: 'Dodaj ksiazke', url: '/add-book', icon: 'book' },
    { title: 'Moja bibloteka', url: '/library', icon: 'library' },
    { title: 'Poszukiwane', url: '/wanted-books', icon: 'heart' },
    { title: 'Czat', url: '/chat', icon: 'chatbox' },
    { title: 'Historia', url: '/archive', icon: 'archive' },
    { title: 'Profil', url: '/user-profil', icon: 'person' },
    { title: 'Intro-Test', url: '/intro', icon: 'person' },
  ];
  public labels = ['Labelka 1', 'Labelka 2', 'Labelka 3'];
  constructor() { }

  logOut(){}
}
