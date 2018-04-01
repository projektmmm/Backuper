import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { User } from './User';
import { RouterLink, Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private http :HttpClient) { 

  }
  ngOnInit() {
    
  }

  Register(IUsername:string,IPassword:string,IEmail:string) {

    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')

    const data: User={
      Username: IUsername,
      Password: IPassword,
      Email: IEmail
    }
    this.http.post('http://localhost:63324/api/admin/register', JSON.stringify(data), head)
    .subscribe(Response=>{
      console.log(Response);
    }
    )}
  }
  


