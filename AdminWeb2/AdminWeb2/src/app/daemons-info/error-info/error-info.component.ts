import { ErrorDetails } from './../interfaces';
import { DaemonsInfoComponent } from './../daemons-info.component';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';


@Component({
  selector: 'error-info',
  templateUrl: './error-info.component.html',
  styleUrls: ['./error-info.component.css']
})
export class ErrorInfoComponent implements OnInit {

  constructor (private http: HttpClient, public dialogRef: MatDialogRef<DaemonsInfoComponent>, public snackBar: MatSnackBar, @Inject(MAT_DIALOG_DATA) public data: any)
    { 
      this.daemonId = data.daemonId;
      this.userName = localStorage.getItem("Username");
      this.getErrors();
      
    }

  ngOnInit() {
  }

  Id: number;
  daemonId: number;
  userName: string;
  errorDetails: ErrorDetails[];
  Name: string;
  canLoad: boolean = false;
  readonly root_URL = 'http://localhost:63324';

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    }); 
  }

  getErrors() {
    this.http.get<ErrorDetails[]>(this.root_URL + "/api/admin/backup-errors/" + this.userName + "-" + this.daemonId).subscribe
    (data => {
      if (data[0].Problem == 'N') {
        this.errorDetails = null;
        return;
      }
       

      this.errorDetails = data;
      this.canLoad = true;
      this.ngOnInit();
    })
  }

  solvedProblem(problemId) {
    this.http.patch(this.root_URL + "/api/admin/backup-errors/" + problemId, problemId).subscribe
    (data => {
      if(data == true) {
        this.openSnackBar("", "Problem was solved!");
        this.getErrors();
      }
    })

  }

  closeDialog() {
    this.dialogRef.close()
  }

  
}
