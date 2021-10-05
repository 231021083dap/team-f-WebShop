import { Component, OnInit } from '@angular/core';
import { Address } from '../_models/address';
import { AddressService } from '../_services/address.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  public addresses : Address[] = []
  public address : Address = {}
  public newAddress : any =
  {
    userId: 13,
    address: "stri22ng",
    zipCode: 0,
    cityName: "string"
  }
  public updateAddressId : number = 3
  public updateAddress : any =
  {
    userId: 13,
    address: "stri22n222g",
    zipCode: 0,
    cityName: "string"
  }

  constructor(private addressService: AddressService) { }

  ngOnInit(): void {
    // console.log('Hello from profile-page')
    
    // this.addressService.getAll().subscribe(a => console.log(a))
    // this.getAll(true)

    // this.addressService.getById(13).subscribe(a => console.log(a))
    // setInterval(()=> {this.getById(5)}, 1000);

    // this.addressService.create(this.newAddress).subscribe(a => console.log(a))
    // this.create(this.newAddress)

    // this.addressService.update(this.updateAddressId, this.updateAddress).subscribe(a => console.log(a))
    // this.update(this.updateAddressId, this.updateAddress)

    // this.addressService.delete(3).subscribe(a => console.log(a))
    // this.delete(4)
  }

  getAll(log : boolean) : void {    
    this.addressService.getAll().subscribe(a=> {
      this.addresses = a
      if (log) console.log(a)
    })
  }

  getById(id : number) : void {
    this.addressService.getById(id).subscribe(a => {this.address = a;})
  }

  create(address: any) : void {
    this.addressService.create(address).subscribe(c => {this.newAddress = c})
  }

  update(id : number, address : any) : void {
    this.addressService.update(id, address).subscribe(a => {this.updateAddress = a})
  }

  delete(id : number) : void {
    this.addressService.delete(id).subscribe(d => console.log(d))
  }
}
