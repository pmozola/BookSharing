import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

import Quagga from 'quagga';

@Component({
  selector: 'app-bar-code-reader',
  templateUrl: './bar-code-reader.component.html',
  styleUrls: ['./bar-code-reader.component.scss'],
})
export class BarCodeReaderComponent implements AfterViewInit {
  @Output() codeBarScaned = new EventEmitter<number>();

  @ViewChild('barCodeReader') barCodeReaderChildView: ElementRef;
  constructor(private changeDetectorRef: ChangeDetectorRef) { }

  ngAfterViewInit(): void {
    if (!navigator.mediaDevices || !(typeof navigator.mediaDevices.getUserMedia === 'function')) {
      //TODO log error
      return;
    }

    Quagga.init({
      inputStream: {
        constraints: {
          facingMode: 'environment'
        },
        area: {
          top: '0%',
          right: '0%',
          left: '0%',
          bottom: '0%'
        }
      },
      decoder: {
        readers: ['ean_reader'],
      },

    },
      (err) => {
        if (err) {
          // TODO log Error
        } else {
          Quagga.start();
          Quagga.onDetected((res) => {
            this.onBarcodeScanned(res.codeResult.code);
          });
        }
      });

    setTimeout(() => {
    }, 10000);
  }

  onBarcodeScanned(code: string) {
    this.pauseBarcodeRecognition();
    this.changeDetectorRef.detectChanges();

    this.codeBarScaned.emit(+code);
  }

  resumeBarcodeRecognition() {
    this.barCodeReaderChildView.nativeElement.getElementsByTagName("video")[0].start();
    Quagga.start();
  }
  pauseBarcodeRecognition() {
    this.barCodeReaderChildView.nativeElement.getElementsByTagName("video")[0].pause();
    Quagga.pause();
  }
}
