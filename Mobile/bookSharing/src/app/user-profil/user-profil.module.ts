import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { UserProfilPageRoutingModule } from './user-profil-routing.module';

import { UserProfilPage } from './user-profil.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    UserProfilPageRoutingModule
  ],
  declarations: [UserProfilPage]
})
export class UserProfilPageModule {}
