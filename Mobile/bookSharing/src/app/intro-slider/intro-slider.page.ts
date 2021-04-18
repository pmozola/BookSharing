import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FirstTimeStorageService } from '../storages/first-time.storage.service';

@Component({
  selector: 'app-intro-slider',
  templateUrl: './intro-slider.page.html',
  styleUrls: ['./intro-slider.page.scss'],
})
export class IntroSliderPage implements OnInit {

  constructor(private firstTimeStorage: FirstTimeStorageService, private router: Router) { }

  ngOnInit() {
  }

  continue() {
    this.firstTimeStorage.saveFirstTimeLoad();
    this.router.navigate(['']);
  }
}
