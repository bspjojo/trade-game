import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment';
import { AppConfig } from './app-config.model';

@Injectable()
export class ConfigService {
    public config: AppConfig = environment;
}
