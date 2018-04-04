import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { User } from './User';
import { RouterLink, Router,ActivatedRoute } from '@angular/router';
import { PARAMETERS } from '@angular/core/src/util/decorators';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    
    constructor(private http :HttpClient,
      private route: ActivatedRoute,
      private router:Router,) {
     
  }
  Login(IUsername:string,IPassword:string) {
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    const data: User={
      Username:IUsername,
      Password:IPassword
    }
    this.http.post('http://localhost:63324/api/admin/login', JSON.stringify(data), head)
    .subscribe(Response=>{
      if(Response)
      {
        localStorage.setItem("LogedIn","true");
        localStorage.setItem("Username",IUsername);
        ///localStorage.setItem("Token",)
        console.log("Done");
        this.router.navigate(['/home'])
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
