import {Router, RouterConfiguration} from 'aurelia-router';
import {PLATFORM} from 'aurelia-pal';

export class App {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Assets';
    config.options.pushState = true;
    config.options.root = '/';
    config.map([
      {route: '', moduleId: PLATFORM.moduleName('pages/home/home')}
    ]);
    this.router = router;
  }

  public message = 'Hello World!ss';
}
