import { ErrorDetails } from './../interfaces';
import { DaemonsInfoComponent } from './../daemons-info.component';
import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
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
  errorDetails: ErrorDetails[] = [];
  errorDetails_notSolved: ErrorDetails[] = [];
  errorDetails_solved: ErrorDetails[] = [];
  tableSource_notSolved: MatTableDataSource<ErrorDetails>;
  tableSource_solved: MatTableDataSource<ErrorDetails>;
  tableSource: MatTableDataSource<ErrorDetails>;
  Name: string;
  canLoad: boolean = false;
  readonly root_URL = 'http://localhost:63324';
  displayedColumns = ['Content'];
  notSolved = "highlight_off";
  solved = "check_circle";

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    }); 
  }

  getErrors() {
    this.http.get<ErrorDetails[]>(this.root_URL + "/api/admin/backup-errors/" + this.userName + "-" + this.daemonId, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))}).subscribe
    (data => {
      if (data[0].Problem == 'N') {
        this.errorDetails = null;
        return;
      }
       
    this.prepareVersions(data); 
      
    this.canLoad = true;
    })
  }

  solvedProblem(problemId) {
    this.http.patch(this.root_URL + "/api/admin/backup-errors/" + problemId, problemId, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))}).subscribe
    (data => {
      if(data == true) {
        this.openSnackBar("", "Problem was solved!");
        this.getErrors();
      }
    })

  }
  
  prepareVersions(data: ErrorDetails[]) {

    this.errorDetails_notSolved = [];
    this.errorDetails_solved = [];

    data.forEach(element => {
      
      if (element.Solved == true) {
        this.errorDetails_solved.push(element);
      }
      else {
        this.errorDetails_solved.push(element);
        this.errorDetails_notSolved.push(element);        
      }
    });

    this.tableSource_notSolved = new MatTableDataSource(this.errorDetails_notSolved);
    this.tableSource_notSolved.paginator = this.paginator;
    this.tableSource_notSolved.sort = this.sort;

    this.tableSource_solved = new MatTableDataSource(this.errorDetails_solved);
    this.tableSource_solved.paginator = this.paginator;
    this.tableSource_solved.sort = this.sort;

    this.tableSource = this.tableSource_notSolved;  
  }

  showSolved(isChecked: boolean) {
    
    if (isChecked == false) {
      this.tableSource = this.tableSource_solved;
    }
    else {
      this.tableSource = this.tableSource_notSolved;
    }

  }

  closeDialog() {
    this.dialogRef.close()
  }

  
}
