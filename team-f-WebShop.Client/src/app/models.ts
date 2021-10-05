export interface category{
  id:number,
  categoryName: string
}

export interface Product{
    productId: number,
    name: string,
    price: number,
    quantity: number,
    description: string
}



export interface User {
    id? : number,
    email? : string,
    password? : string,
    role? : Role,
    orderList? : OrderList[],
    address? : Address[]
}

export enum Role {
    User = 'User',
    Admin = 'Admin'
}

export interface OrderList {
    id : number,
    orderDateTime : Date,
    userId : number,
    user? : User
}

export interface Address {
    id?:number,
    address?:string,
    zipCode?:number,
    cityName?:number,
    user? : User
}

