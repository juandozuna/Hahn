export enum AssetDeparment {
  HQ = 0,
  Store1 = 1,
  Store2 = 2,
  Store3 = 3,
  MaintenanceStation = 4
}


export function GetDepartmentsList(): any {
  return Object
    .keys(AssetDeparment)
    .map(key => AssetDeparment[key])
    .filter(value => typeof value === 'string')
    .map(
      (val, index) => {
        return {
          value: index,
          text: val
        }
      });
}

/**
 * {
  "id": 0,
  "assetName": "string",
  "department": 0,
  "countryOfDepartment": "string",
  "emailAddressOfDepartment": "string",
  "purchaseDate": "2021-06-26T03:45:49.617Z",
  "broken": true
}
 */
