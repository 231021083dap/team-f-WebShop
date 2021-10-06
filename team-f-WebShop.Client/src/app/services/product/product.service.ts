

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from 'src/app/models'
import { Observable } from 'rxjs'


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = "https://localhost:5001/api/Product";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) { }


  getAllProductsService(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductByIdService(productId: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${productId}`);
  }

  // GET PRODUCT BY CATEGORY
  //getProductByCategoryIdService(Id: number): Observable<Product> {
  //  return this.http.get<Product>(`${this.apiUrl}category/${Id}`);
  //}

  addProductService(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product, this.httpOptions);
  }

  updateProductService(productId: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${productId}`, product, this.httpOptions);
  }

  deleteProduct(productId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${productId}`, this.httpOptions);
  }
}