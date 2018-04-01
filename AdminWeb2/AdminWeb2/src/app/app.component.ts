import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { RouterLink, Route } from '@angular/router';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { User } from './login/User';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  LoggedIn = true;
  Token : string;
  
  constructor(private rout:Router,public location: Location){
    console.log();
  }
   ngOnInit() {
    if (localStorage.getItem("LogedIn")=="true")
    {
      this.LoggedIn = true;
    }
    else
    {
      this.LoggedIn = false; 
      if(this.location.path() != "/register")
      {
        this.rout.navigateByUrl("/")
      }
    }
  }
}