import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { ISubCategory } from '../shared/models/subcategory';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {


  @ViewChild('search', { static: true })
  searchTerm!: ElementRef;

  products: IProduct[] = [];
  brands: IBrand[] = [];
  subCategories: ISubCategory[] = [];
 shopParams = new ShopParams();
  totalCount: number = 0;

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to high', value: 'priceAsc'},
    {name: 'Price: High to low', value: 'priceDesc'},
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getSubCategories();
  }
  getProducts(){
    this.shopService.getProducts(this.shopParams)
      .subscribe(response => {
      this.products = response!.data;
      this.shopParams.pageNumber = response!.pageIndex;
      this.shopParams.pageSize = response!.pageSize;
      this.totalCount = response!.count;

    }, err => console.log(err));
  }
  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, err => console.log(err));
  }
  getSubCategories(){
    this.shopService.getSubCategories().subscribe(response => {
      this.subCategories = [{id: 0, name: 'All'}, ...response];
    }, err => console.log(err));
  }
  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onSubCategorySelected(subCategoryId: number){
    this.shopParams.subCategoryId = subCategoryId;
    this.shopParams.pageNumber = 1;

    this.getProducts();
  }
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }
  onPageChanged(event: number){
    if(this.shopParams.pageNumber != event){
      this.shopParams.pageNumber = event;
      this.getProducts();

    }
  }
  onSearch(){
   this.shopParams.search = this.searchTerm.nativeElement.value;
   this.shopParams.pageNumber = 1;
   
   this.getProducts();
  }
  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
