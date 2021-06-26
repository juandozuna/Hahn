import { Asset } from "models/asset.model";
import { BaseService } from "./base-service";


export class AssetService extends BaseService {
 
  async getAllAssets(): Promise<Asset[]> {
    const type = await this.client.get<Asset[]>('/assets');
    return type.data;
  }
}
