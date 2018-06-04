import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {AdminChange} from './../Change';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.css']
})
export class ChangeEmailComponent implements OnInit {

  constructor(private http :HttpClient,
    public snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ChangeEmailComponent>) {
      
     }
  ErrorText : string;
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
     this.http.patch<boolean>("http://localhost:63324/api/admin/AdminSettings/newEmail",JSON.stringify(data),head)
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
