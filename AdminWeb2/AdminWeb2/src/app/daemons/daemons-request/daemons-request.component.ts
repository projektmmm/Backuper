import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule,HttpHeaders} from '@angular/common/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { RouterLink, Router,ActivatedRoute } from '@angular/router';
import { Jsonp } from '@angular/http';
import { Daemons } from '.././daemons';
import { rowIdService } from './../../daemons-info/service';

@Component({
  selector: 'app-daemons-request',
  templateUrl: './daemons-request.component.html',
  styleUrls: ['./daemons-request.component.css']
})
export class DaemonsRequestComponent implements OnInit {

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  displayedColumns = ['Id', 'Name', 'Description', 'Funcbuttons'];
  tableSource: MatTableDataSource<Daemons>;
  NewRequest: boolean;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private http: HttpClient, public snackBar: MatSnackBar, private rowIdService: rowIdService, private router: Router,public dialog: MatDialog,public dialogRef: MatDialogRef<DaemonsRequestComponent>) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
    
    this.getDaemons();
   }

  ngOnInit() {
    this.getDaemons();
  }
  getDaemons() {
    this.http.get<Daemons[]>(this.root_URL + "/api/admin/daemons/AddDaemon/" + localStorage.getItem("Username")).subscribe
    (data => {

      this.initializeTable(data);

    });
    
}
DeleteDaemon(Row: number){
this.http.delete<boolean>(this.root_URL+"/api/admin/daemons/DeleteDaemon/"+Row)
.subscribe(Response=>{
  this.dialogRef.close()
})
}
VerifyDaemon(Row: number){
  
 this.http.patch<boolean>(this.root_URL+"/api/admin/daemons/AddDaemon/"+Row,Row)
 .subscribe(Response=>{
   if (Response)
   {
      this.dialogRef.close()
    
   }
   else
   {
     console.log("Fuck...")
   }
 })
}
private initializeTable(data: Daemons[]) {

  this.tableSource = new MatTableDataSource(data);
  this.tableSource.paginator = this.paginator;
  this.tableSource.sort = this.sort;    
}
}
