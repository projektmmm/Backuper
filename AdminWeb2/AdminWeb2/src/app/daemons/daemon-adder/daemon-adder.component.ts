import { Component, OnInit } from '@angular/core';
import {NewDaemon,User} from './NewDaemon';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { RouterLink, Router,ActivatedRoute } from '@angular/router';
import { Jsonp } from '@angular/http';

@Component({
  selector: 'app-daemon-adder',
  templateUrl: './daemon-adder.component.html',
  styleUrls: ['./daemon-adder.component.css']
})
export class DaemonAdderComponent implements OnInit {

  constructor(private http :HttpClient,private router:Router,public dialogRef: MatDialogRef<DaemonAdderComponent>) { 
    
  }

  AdminName :string;
  ID : number;
  
  ngOnInit() {
    this.AdminName = localStorage.getItem("Username")
    const data: User={
      Username:this.AdminName
    }
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    
    this.http.post<number>("http://localhost:63324/api/admin/daemons/GetId",JSON.stringify(data),{headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
    .subscribe(Response =>{
      this.ID = Response
    })
  }

  Close(){
    this.dialogRef.close()
  }
  AddDaemon(Nickname:string,DaemonTo:string,Description:string,){
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    
    
    const data:NewDaemon = {
      UserId: this.ID,
      Name:Nickname,
      DaemonTo:DaemonTo,
      Description:Description,
    }

    
    this.http.post("http://localhost:63324/api/admin/daemons/AddDaemon",JSON.stringify(data),{headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
    .subscribe(Response => {
      console.log(Response)
    })
    this.dialogRef.close()
    }
  }

