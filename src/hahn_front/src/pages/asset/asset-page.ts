
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { inject } from 'aurelia-dependency-injection';
import { AssetDeparment, GetDepartmentsList } from 'enums/asset-deparment.enum';
import { Asset } from 'models/asset.model';

@inject(Router)
export class AssetPage {
  isEditing = false;
  departments: any[] = [];
  
  asset = {
    broken: false,
    id: 0,
    assetName: '',
    department: 2,
    countryOfDeparment: '',
    emailAddressOfDepartment: '',
    purchaseDate: new Date()
  }

  constructor(private router: Router) {
    
  }

  activate(params: any, routeConfig: RouteConfig, navigationInstructions: NavigationInstruction) {
    console.log('ID', params);
    this.startPage();
  }

  private startPage() {
    this.loadDepartments();
  }
  
  private loadDepartments() {
    this.departments = GetDepartmentsList();
  }
  
  onSubmit() {
    console.log(this.asset);
  }

}
