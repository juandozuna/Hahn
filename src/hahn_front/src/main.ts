import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import 'bootstrap/dist/css/bootstrap.css';
import '@fortawesome/fontawesome-free/css/all.css';
import {Backend, I18N, TCustomAttribute } from 'aurelia-i18n';

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

  aurelia.use.plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
    const aliases = ['t', 'i18n'];
    TCustomAttribute.configureAliases(aliases);
    instance.i18next.use(Backend);
    return instance.setup({
      backend: {
        loadPath: './locales/{{lng}}//{{ns}}.json'
      },
      ns: 'global',
      defaultNS: 'global',
      fallbackNS: false,
      attributes: aliases,
      lng: 'en',
      whitelist: ['en'],
      preload: ['en'],
      fallbackLng: 'en',
      debug: true
    });
  });  
    
  aurelia.use.plugin(PLATFORM.moduleName('aurelia-validation'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
