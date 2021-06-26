import { Asset } from '../../models/asset.model';
import { AssetService } from '../../services/asset.service';
import { inject } from 'aurelia-dependency-injection';

@inject(AssetService)
export class Home {
  static inject = [AssetService];

  assets: Asset[] = [];

  constructor(private assetService: AssetService) {
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
  
  delete(id: number): void {
    console.log('DELETE', id);
  }

  update(id: number): void {
    console.log('UPDATE', id);
  }
}
