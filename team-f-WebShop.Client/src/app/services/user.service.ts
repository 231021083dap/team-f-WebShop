import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../_models/user';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class UserService {
  private endPoint = 'https://localhost:5001/api/User';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  // GET all users
  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.endPoint, this.httpOptions);
  }

  // GET user by ID
  getById(id : number) : Observable<User> {
    let x = this.http.get<User>(`${this.endPoint}/${id}`, this.httpOptions)
    console.log(x)

    return this.http.get<User>(`${this.endPoint}/${id}`, this.httpOptions)
  }

  // POST https://localhost:5001/api/User/Create
  create(user : User) : Observable<User> {

    // Opretter objeckt som er kompatibelt med json swagger input:
    // {
    //   "email": "victor",
    //   "password": "Password"
    // }
    let x = {email: user.email, password: user.password}
    return this.http.post(`${this.endPoint}/Create`, x, this.httpOptions)
  }


  update(id : number, user : User) : Observable<User> {

    let x = {
      email: user.email, 
      password: user.password,
      role: user.role
    }

    return this.http.put(`${this.endPoint}/${id}`, x, this.httpOptions)
  }

  delete(id : number) : Observable<any> {
    return this.http.delete(`${this.endPoint}/${id}`, this.httpOptions)
  }

}
