import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IntroSliderPage } from './intro-slider.page';

const routes: Routes = [
  {
    path: '',
    component: IntroSliderPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class IntroSliderPageRoutingModule {}
