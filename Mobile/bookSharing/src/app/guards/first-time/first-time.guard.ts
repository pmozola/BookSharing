import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { FirstTimeStorageService } from 'src/app/storages/first-time.storage.service';

@Injectable({
  providedIn: 'root'
})
export class FirstTimeGuard implements CanActivate {

  constructor(private storage: FirstTimeStorageService, public router: Router) {
  }

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Promise<boolean> {
    if (await this.storage.isFirstTimeLoad()) {
      this.router.navigate(['intro']);
      return false;
    }

    return true;
  }

}
