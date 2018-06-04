import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {AdminChange} from './../Change';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  constructor(private http :HttpClient,
    public snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ChangePasswordComponent>) {
      
     }
  ngOnInit() {
  }
  Email(Password,Email,Email2)
  {
    if (Email == Email2)
    {
      const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
      head.headers.append('Content-Type', 'application/json')
      const data: AdminChange={
        Username: localStorage.getItem("Username"),
        Password: Password,
        Content: Email
      }
     this.http.patch<boolean>("http://localhost:63324/api/admin/AdminSettings/newPassword",JSON.stringify(data), {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
     .subscribe(data=>{
      if (data)
      {
        this.openSnackBar("","Email Changed")
        this.dialogRef.close();
      }
      else
      {
        this.openSnackBar("","Wrong password")
      }})
    }
    else
    {
      this.openSnackBar("","Emails arent same")
    }
  }
  closeDialog()
  {
    this.dialogRef.close();
    console.log("Ahoj")
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    })}
}
