import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage-angular';

@Injectable({
    providedIn: 'root'
})
export class FirstTimeStorageService {
    private storageFirstTimeTokenName: string = 'firstTime';
    firstTimeStorage: Storage;

    constructor(private storage: Storage) {
        storage.create()
    }

    async init() {
        this.firstTimeStorage = await this.storage.create();
    }

    saveFirstTimeLoad(): void {
        this.firstTimeStorage.set(this.storageFirstTimeTokenName, true);
    }

    async isFirstTimeLoad(): Promise<boolean> {
        if (!this.firstTimeStorage) {
            await this.init()
        };

        const result = await this.firstTimeStorage.get(this.storageFirstTimeTokenName);

        if (result != null) {
            return false;
        }

        return true;
    }
}