import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Purchasehistory } from '../Models/purchasehistory';
import { Cart } from '../Models/cart';
const Requestheaders={headers:new HttpHeaders({
  'Content-Type':'application/json',
  'Authorization':'Bearer'+localStorage.getItem('token')
})}

@Injectable({
  providedIn: 'root'
})
export class ItemsService {
  constructor(private http:HttpClient) { }
  url2:string='http://localhost:49190/Items/'
  public SearchItems(name:string):Observable<any>
  {
    return this.http.get<any>(this.url2+'SearchItems/'+name,Requestheaders)
  }
  public SearchItemByCategory(name:string):Observable<any>
  {
    return this.http.get<any>(this.url2+'SearchItemByCategory/'+name,Requestheaders)
  }
  public SearchItemBySubCategory(name:string):Observable<any>
  {
    return this.http.get<any>(this.url2+'SearchItemBySubCategory/'+name,Requestheaders)
  }
  public BuyItem(purchase:Purchasehistory):Observable<any>{
    return this.http.post<any>(this.url2+'BuyItem',purchase,Requestheaders)
  }
  public GetCategory():Observable<any>{
    return this.http.get<any>(this.url2+'GetCategory',Requestheaders);
  }
  public PurchaseHistory(buyerid:number):Observable<any>{
    return this.http.get<any>(this.url2+'View/'+buyerid,Requestheaders);
  }
  public AddtoCart(cart:Cart):Observable<Cart>{
    return this.http.post<Cart>(this.url2+'AddtoCart',cart,Requestheaders);
  }
  public DeleteCart(id:number):Observable<any>{
    return this.http.delete<any>(this.url2+'DeleteCart/'+id,Requestheaders);
  }
  public GetCart(bid:number):Observable<any>{
    return this.http.get<any>(this.url2+'GetCart/'+bid,Requestheaders);
  }
  public GetCount(bid:number):Observable<any>{
    return this.http.get<any>(this.url2+'GetCount/'+bid,Requestheaders);
  }
}
