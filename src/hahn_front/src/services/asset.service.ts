import { Asset } from "models/asset.model";
import { BaseService } from "./base-service";


export class AssetService extends BaseService {
 
  async getAllAssets(): Promise<Asset[]> {
    const type = await this.client.get<Asset[]>('/assets');
    return type.data;
  }

  async getAsset(id: number): Promise<Asset> {
    const result = await this.client.get<Asset>(`/assets/${id}`);
    if (result.status >= 400) throw Error(JSON.stringify(result.data));
    return result.data;
  }

  async saveAsset(asset: Asset): Promise<any> {
    const result = await this.client.post('/assets', asset);
    if (result.status >= 400) throw Error(JSON.stringify(result.data));
    return result.data;
  }

  async updateAsset(asset: Asset): Promise<any> {
    const result = await this.client.put('/assets', asset);
    if (result.status >= 400) throw Error(JSON.stringify(result.data));
    return result.data;
  }

  async deleteAsset(id: number): Promise<Asset> {
    const result = await this.client.delete(`/assets/${id}`);
    if (result.status >= 400) throw Error(JSON.stringify(result.data));
    return result.data;
  }
}
