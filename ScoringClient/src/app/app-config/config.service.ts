import { Injectable } from '@angular/core';
import { AppConfig } from './app-config.model';
import { environment } from '../../environments/environment';

@Injectable()
export class ConfigService {
    public config: AppConfig = environment;
}
