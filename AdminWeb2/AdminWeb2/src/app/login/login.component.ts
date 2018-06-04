import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { User } from './User';
import { RouterLink, Router,ActivatedRoute } from '@angular/router';
import { PARAMETERS } from '@angular/core/src/util/decorators';
import {MatSnackBar} from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    
    constructor(private http :HttpClient,
      public snackBar: MatSnackBar,
      private route: ActivatedRoute,
      private router:Router,) {
     
  }
  arrayResponse: string[] = [];
  
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    })};

  Login(IUsername:string,IPassword:string) {
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')

    const data: User= {
      Username: IUsername,
      Password: IPassword
    }
    
    this.http.post<string>('http://localhost:63324/api/admin/login', JSON.stringify(data), head)
    .subscribe(Response=>{
      if(Response!="false")
      {
        this.arrayResponse = Response.split("@@border@@");

        localStorage.setItem("LogedIn", "true");
        localStorage.setItem("Username", this.arrayResponse[1]);
        localStorage.setItem("Token", this.arrayResponse[0])
        
        this.openSnackBar("","Login Succeded")
        this.router.navigate(['/home'])
      }
      else{
        console.log("fail")
        this.openSnackBar("","Login Failed")
      }
    }
    )}

  ngOnInit() {
    
  }

}
