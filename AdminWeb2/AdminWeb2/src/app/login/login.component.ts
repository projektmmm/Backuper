import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { User } from './User';
import { RouterLink, Router } from '@angular/router';
import { query } from '@angular/core/src/animation/dsl';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    
    constructor(private http :HttpClient) {
     
  }
  Login(IUsername:string,IPassword:string) {
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    const data: User={
      Username:IUsername,
      Password:IPassword
    }
    this.http.post<boolean>('http://localhost:54736/api/admin/login',data,head)
    .subscribe(Response=>{
      if(Response)
      {
        localStorage.setItem("Data",JSON.stringify(Response));
        console.log("Done");
      }
      else{
        console.log("Nope")
      }
    }
    )}
  Register(){
    
  }
  ngOnInit() {
    
  }

}
