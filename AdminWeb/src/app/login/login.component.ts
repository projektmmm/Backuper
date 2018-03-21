import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import {LoginInfo} from './LoginInfo'
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { PARAMETERS } from '@angular/core/src/util/decorators';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  readonly Root_URL = 'http://localhost:54736'; /*http://localhost:54736 */
  /*posts: Observable<any>;*/
  
  newPost:any;
  LoginAnsw:any;
  constructor(private http: HttpClient) {}
  ngOnInit() {
  }
login(Login,Password){
  {
    const data: LoginInfo ={
      Username: Login,
      Password: Password
    }
  this.http.get<string>(this.Root_URL+"api/admin/login",
  {headers: { 'Content-Type': 'application/json'}})
  .subscribe(responce => {
    this.LoginAnsw = JSON.parse(responce);
    console.log(this.LoginAnsw);
  })
}
    
    

  

}}
