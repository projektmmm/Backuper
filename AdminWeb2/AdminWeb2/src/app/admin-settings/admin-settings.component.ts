import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { DataTableResource } from 'angular5-data-table';
import { Daemons } from './../daemons/daemons';
import { rowIdService } from './../daemons-info/service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ChangeEmailComponent } from './change-email/change-email.component';

@Component({
  selector: 'app-admin-settings',
  templateUrl: './admin-settings.component.html',
  styleUrls: ['./admin-settings.component.css']
})
export class AdminSettingsComponent implements OnInit {

  constructor(private http: HttpClient, public snackBar: MatSnackBar, private rowIdService: rowIdService,public dialog: MatDialog) { }
  readonly root_URL = 'http://localhost:63324';
  Username: string = localStorage.getItem("Username")
  Email: string 
  displayedColumns = ['Name', 'Description','SendSMS','SendEmail'];
  tableSource: MatTableDataSource<Daemons>;
  checked: boolean = true;
  Foo: number
  ngOnInit() {
    this.http.get<string>(this.root_URL+"/api/admin/AdminSettings/"+localStorage.getItem("Username"), {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
    .subscribe  (data => {
      this.Email = data
    })
    this.http.get<Daemons[]>(this.root_URL+"/api/admin/daemons/"+this.Username, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
    .subscribe  (data => {
      this.tableSource = new MatTableDataSource(data);
    })
  }

  ChangePassword()
  {
    let dialogRef = this.dialog.open(ChangePasswordComponent, {
      width: "400px"
    })
  }
  ChangeEmail()
  {
    let dialogRef = this.dialog.open(ChangeEmailComponent, {
      width: "400px"
    })
  }
  
  SendEmail(IDaemonId: number)
  {
    this.http.get(this.root_URL+"/api/admin/AdminSettings/Email/"+IDaemonId, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
    .subscribe (data => {
    })
  }
  SendSMS(DaemonId: number)
  {
    this.http.get(this.root_URL+"/api/admin/AdminSettings/Sms/"+DaemonId, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
    .subscribe (data => {
    })
  }
}
