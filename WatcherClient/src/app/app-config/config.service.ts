import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppConfig } from './app-config.model';

@Injectable()
export class ConfigService {
    private conf: Promise<AppConfig>;

    constructor(private httpClient: HttpClient) { }

    public getConfig(force: boolean = false): Promise<AppConfig> {
        if (this.conf == null || force === true) {
            this.conf = this.httpClient.get<AppConfig>('/assets/config/conf.json').toPromise();
        }

        return this.conf;
    }
}
