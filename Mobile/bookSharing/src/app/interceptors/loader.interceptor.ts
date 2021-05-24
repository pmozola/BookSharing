import { Injectable } from '@angular/core';
import {
  HttpResponse,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoadingController } from '@ionic/angular';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  private requests: HttpRequest<any>[] = [];
  private loader: Promise<void>;

  constructor(public loadingController: LoadingController) {
  }

  removeRequest(req: HttpRequest<any>) {
    const i = this.requests.indexOf(req);
    if (i >= 0) {
      this.requests.splice(i, 1);
    }

    if (this.requests.length == 0 && this.loader) {
      this.hideLoader();
    }
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.requests.push(req);

    this.showLoader();

    return Observable.create(observer => {
      const subscription = next.handle(req)
        .subscribe(
          event => {
            if (event instanceof HttpResponse) {
              this.removeRequest(req);
              observer.next(event);
            }
          },
          err => {
            this.removeRequest(req);
            observer.error(err);
          },
          () => {
            this.removeRequest(req);
            observer.complete();
          });

      return () => {
        this.removeRequest(req);
        subscription.unsubscribe();
      };
    });
  }

  showLoader() {
    if (this.loadingController) {
      this.loader = this.loadingController.create({
        message: ''
      }).then((res) => {
        res.present();

        res.onDidDismiss().then((dis) => {
        });
      });
    }
  }

  hideLoader() {
    this.loadingController.dismiss();
  }
}