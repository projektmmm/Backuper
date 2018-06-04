import { AppModule } from './../app.module';
import { rowIdService } from './../daemons-info/service';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Daemons } from './daemons';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { DaemonAdderComponent } from './daemon-adder/daemon-adder.component';
import { DaemonsRequestComponent } from './daemons-request/daemons-request.component';

@Component({
  selector: 'daemons',
  templateUrl: './daemons.component.html',
  styleUrls: ['./daemons.component.css']
})
export class DaemonsComponent implements OnInit {
  
  readonly root_URL = 'http://localhost:63324';
  NewRequest : boolean
  displayedColumns = ['Id', 'Name', 'Description', 'Funcbuttons'];
  tableSource: MatTableDataSource<Daemons>;
  headers = new HttpHeaders();
  

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private http: HttpClient, public snackBar: MatSnackBar, private rowIdService: rowIdService, private router: Router,public dialog: MatDialog) {
    this.headers.append("Token", localStorage.getItem("Token"));
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
    this.newRequests();
    this.getDaemons();
  }

  
   ngOnInit() {
     this.newRequests();
     this.getDaemons();
  }

  ngAfterViewInit() {
    if (this.tableSource != null)
      this.tableSource.sort = this.sort;
  }

  getDaemons() {
    this.http.get<Daemons[]>(this.root_URL + "/api/admin/daemons/" + localStorage.getItem("Username"), {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))}).subscribe
    (data => {

      this.initializeTable(data);

    });
  }
  newRequests(){
      this.http.get<boolean>(this.root_URL+"/api/admin/daemons/Get"+localStorage.getItem("Username"), {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))})
      .subscribe
      (Response=>{
        this.NewRequest = Response
      })
  }
  AddDaemon(){
    let dialogRef = this.dialog.open(DaemonAdderComponent, {
      width: "400px"
    })

  }
  Requests(){
    let dialogRef = this.dialog.open(DaemonsRequestComponent, {
      width: "600px"
    })
  }
  private initializeTable(data: Daemons[]) {

    this.tableSource = new MatTableDataSource(data);
    this.tableSource.paginator = this.paginator;
    this.tableSource.sort = this.sort;    
  }


   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    });
  }  

  passRowIdToService(rowId: number) {
    this.rowIdService.rowId = rowId;
    console.log(this.rowIdService.rowId);
    this.router.navigate(['./daemons-info', rowId]);
  }

}