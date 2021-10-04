import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from 'src/app/_models/address';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private endPoint = 'https://localhost:5001/api/Address';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  constructor(private http: HttpClient) { }

  // GET all
  getAll() : Observable<Address[]> {
    console.log("Hello Wolrd")
    return this.http.get<Address[]>(this.endPoint, this.httpOptions);
  }


  getById(id : number) : Observable<Address> {
    console.log("hello from getbyid " + `${this.endPoint}/${id}`);
    
    return this.http.get<Address>(`${this.endPoint}/${id}`, this.httpOptions)
  }

  // POST https://localhost:5001/api/User
  create(address : any) : Observable<Address> {
    return this.http.post(`${this.endPoint}`, address, this.httpOptions)
  }

    
  update(id : number, address : any) : Observable<Address> {
    return this.http.put(`${this.endPoint}/${id}`, address, this.httpOptions)
  }
  

  delete(id : number) : Observable<any> {
    return this.http.delete(`${this.endPoint}/${id}`, this.httpOptions)
  }
}
