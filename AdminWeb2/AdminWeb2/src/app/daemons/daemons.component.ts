import { AppModule } from './../app.module';
import { rowIdService } from './../daemons-info/service';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Daemons } from './daemons';
import { Router } from '@angular/router';

@Component({
  selector: 'daemons',
  templateUrl: './daemons.component.html',
  styleUrls: ['./daemons.component.css']
})
export class DaemonsComponent implements OnInit {
  
  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  displayedColumns = ['Id', 'Name', 'Description', 'Funcbuttons'];
  tableSource: MatTableDataSource<Daemons>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private http: HttpClient, public snackBar: MatSnackBar, private rowIdService: rowIdService, private router: Router) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
    this.getDaemons();
  }

   ngOnInit() {
     this.getDaemons();
  }

  ngAfterViewInit() {
    if (this.tableSource != null)
      this.tableSource.sort = this.sort;
  }

  getDaemons() {
    this.http.get<Daemons[]>(this.root_URL + "/api/admin/daemons/" + localStorage.getItem("Username")).subscribe
    (data => {

      this.initializeTable(data);

    });
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