import { Component, OnInit,Input } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { User } from './User';
import { RouterLink, Router } from '@angular/router';
import {FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatSnackBar} from '@angular/material';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || isSubmitted||control.touched));
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private http :HttpClient, public snackBar: MatSnackBar) { 

    
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    });
  }
  EmailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  

  UsernameFormControl = new FormControl('', [
    Validators.required,
  ]);
 

  PasswordFormControl = new FormControl('', [
    Validators.required,
  ]);

  Emailmatcher = new MyErrorStateMatcher();
  Usernamematcher = new MyErrorStateMatcher();
  Passwordmatcher = new MyErrorStateMatcher();

  ngOnInit() {
    
  }

 
  @Input() email: string;
  Register(IUsername:string,IPassword:string,IEmail:string) {

    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')

    const data: User={
      Username: IUsername,
      Password: IPassword,
      Email: IEmail
    }
    this.http.post<boolean>('http://localhost:63324/api/admin/register', JSON.stringify(data), head)
    .subscribe(Response=>{
      if(Response){
        this.openSnackBar("","Register Succeded")
      }
      else{
        this.openSnackBar("","User already exists")
      }
    }
    )}
  }
  


