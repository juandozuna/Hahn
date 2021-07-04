
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { inject } from 'aurelia-dependency-injection';
import { AssetDeparment, GetDepartmentsList } from 'enums/asset-deparment.enum';
import { Asset } from 'models/asset.model';
import { AssetService } from '../../services/asset.service';
import { ValidationRules, ValidationControllerFactory, validateTrigger, ValidationController } from 'aurelia-validation';

@inject(Router, AssetService, ValidationControllerFactory)
export class AssetPage {
  isEditing = false;
  departments: any[] = [];
  
  asset: Asset = {
    broken: false,
    id: 0,
    assetName: 'i',
    department: 2,
    countryOfDepartment: '',
    emailAddressOfDepartment: '',
    purchaseDate: new Date()
  }
  
  validationRules = ValidationRules
    .ensure('assetName')
    .minLength(4)
    .required()
    .ensure('department')
    .required()
    .ensure('countryOfDepartment')
    .required()
    .ensure('emailAddressOfDepartment')
    .required()
    .email()
    .ensure('purchaseDate')
    .required();

  controller: ValidationController = null;


  constructor(private router: Router, private assetService: AssetService, private validationControllerFactory: ValidationControllerFactory) {
    this.controller = validationControllerFactory.createForCurrentScope()
  }

  activate(params: any, routeConfig: RouteConfig, navigationInstructions: NavigationInstruction) {
    console.log('ID', params);
    this.startPage();
    this.loadCurrentAsset(params.id);
  }

  private startPage() {
    this.loadDepartments();
  }
  
  private loadDepartments() {
    this.departments = GetDepartmentsList();
  }

  private loadCurrentAsset(id: number) {
    this.loadCurrentAssetAsync(id)
      .catch(err => console.log(err));
      //TODO: Show error dialog
  }
  
  private async loadCurrentAssetAsync(id: number) {
    if (id == 0) return;
    const result = await this.assetService.getAsset(id);
    console.log(result);
    this.asset = result;
    this.isEditing = true;
  }
  
  onSubmit() {
    this.onSubmitAsync()
    .then(() => this.navigateBack())  
    .catch(err => console.log(err));
    //TODO: Show error dialog
  }

  async onSubmitAsync() {
    console.log('onSubmit()', this.asset);
    const result = await this.controller.validate({object: this.asset, rules: this.validationRules});
    if (!result.valid) {
      const property = result.results[0].propertyName;
      const firstErrorMessage = result.results[0].message
      alert(`${property}: ${firstErrorMessage}`)
      return;
    }
    if (this.isEditing) {
      await this.updateAsset();
      return;
    }
    await this.createAsset();
  }

  private async createAsset() {
    await this.assetService.saveAsset(this.asset);
  }

  private async updateAsset() {
    await this.assetService.updateAsset(this.asset)
  }

  private navigateBack() {
    this.router.navigateBack();
  }

}
