


import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { category } from 'src/app/models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:5001/api/category';

  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http:HttpClient) { }

  getCategorys(): Observable<category[]>{
    return this.http.get<category[]>(this.apiUrl);
  }

  getCategory(categoryId:number): Observable<category>{
    return this.http.get<category>(`${this.apiUrl}/${categoryId}`);
  }

  addCategory(category:category): Observable<category>{
   return this.http.post<category>(this.apiUrl, category, this.httpOptions);
  }

  updateCategory(categoryId:number, category:category): Observable<category>{
    return this.http.put<category>(`${this.apiUrl}/${categoryId}`, category, this.httpOptions);
  }

  deleteCategory(categoryId:number):Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}/${categoryId}`);
  }
}