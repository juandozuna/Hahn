import { AssetDeparment } from "enums/asset-deparment.enum";

export interface Asset {
  id: number,
  assetName: string,
  department: AssetDeparment,
  countryOfDepartment: string,
  emailAddressOfDepartment: string,
  purchaseDate: Date,
  broken: boolean
}
