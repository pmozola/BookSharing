import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { IntroSliderPageRoutingModule } from './intro-slider-routing.module';

import { IntroSliderPage } from './intro-slider.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    IntroSliderPageRoutingModule
  ],
  declarations: [IntroSliderPage]
})
export class IntroSliderPageModule {}
