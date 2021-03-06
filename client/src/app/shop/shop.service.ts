import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { ISubCategory } from '../shared/models/subcategory';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';
@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {

    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.subCategoryId !== 0) {
      params = params.append('subCategoryId', shopParams.subCategoryId.toString());
    }
    if(shopParams.search != ''){
      params = params.append('search', shopParams.search);
    }
    if(shopParams.sort != 'name'){
      params = params.append('sort', shopParams.sort);
    }
      params = params.append('pageIndex', shopParams.pageNumber),
      params = params.append('pageSize', shopParams.pageSize);

      return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }
  getProduct(id: number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }
  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
  getSubCategories() {
    return this.http.get<ISubCategory[]>(this.baseUrl + 'products/subcategories');
  }
}
