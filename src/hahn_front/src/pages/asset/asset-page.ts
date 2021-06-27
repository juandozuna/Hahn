
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { inject } from 'aurelia-dependency-injection';
import { AssetDeparment, GetDepartmentsList } from 'enums/asset-deparment.enum';
import { Asset } from 'models/asset.model';
import { AssetService } from '../../services/asset.service';

@inject(Router, AssetService)
export class AssetPage {
  isEditing = false;
  departments: any[] = [];
  
  asset: Asset = {
    broken: false,
    id: 0,
    assetName: 'i',
    department: 2,
    countryOfDeparment: '',
    emailAddressOfDepartment: '',
    purchaseDate: new Date()
  }

  constructor(private router: Router, private assetService: AssetService) {
    
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
