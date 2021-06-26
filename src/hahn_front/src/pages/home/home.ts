import { Asset } from '../../models/asset.model';
import { AssetService } from '../../services/asset.service';
import { inject } from 'aurelia-dependency-injection';
import { Router } from 'aurelia-router';

@inject(AssetService, Router)
export class Home {
  static inject = [AssetService];

  assets: Asset[] = [];

  constructor(private assetService: AssetService, private router: Router) {
    this.assets = [];
  }

  created(owingView: any, myView: any): void {
    this.loadData();
  }

  private async loadData() {
    const result = await this.assetService.getAllAssets();
    console.log('assets', result);
    this.assets = [...result];
  }

  create() {
    this.router.navigate(`#/assets/0`);
  }
  
  delete(id: number): void {
    console.log('DELETE', id);
  }

  update(id: number): void {
    console.log('UPDATE', id);
    this.router.navigate(`#/assets/${id}`);
  }
}
