import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import Quagga from 'quagga';

@Component({
  selector: 'app-bar-code-reader',
  templateUrl: './bar-code-reader.component.html',
  styleUrls: ['./bar-code-reader.component.scss'],
})
export class BarCodeReaderComponent implements AfterViewInit {
  @Output() codeBarScaned = new EventEmitter<number>();
  recognizedBarcode: number;

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
          facingMode: 'environment',
          height: '200px'
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
    this.recognizedBarcode = +code;

    this.codeBarScaned.emit(+code);
  }

  resumeBarcodeRecognition() {
    this.barCodeReaderChildView.nativeElement.getElementsByTagName("video")[0].play();
    setTimeout(function () {
      Quagga.start();
    }, 10);
  }

  pauseBarcodeRecognition() {
    this.barCodeReaderChildView.nativeElement.getElementsByTagName("video")[0].pause();
    Quagga.pause();
  }

  retry(): void {
    this.recognizedBarcode = null;
    this.codeBarScaned.emit(null);
    this.resumeBarcodeRecognition();
  }
}
