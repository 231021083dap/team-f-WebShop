import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { category } from './models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:5001/api/category';

  constructor(private http:HttpClient) { }

  getCategorys(): Observable<category[]>{
    return this.http.get<category[]>(this.apiUrl);
  }
}
