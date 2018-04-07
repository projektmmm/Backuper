import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Daemons } from './daemons';

@Component({
  selector: 'daemons',
  templateUrl: './daemons.component.html',
  styleUrls: ['./daemons.component.css']
})
export class DaemonsComponent implements OnInit {
  
  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  displayedColumns = ['Id'];
  tableSource: MatTableDataSource<Daemons>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private http: HttpClient, public snackBar: MatSnackBar) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
  }

   ngOnInit() {
  }

  ngAfterViewInit() {
    if (this.tableSource != null)
      this.tableSource.sort = this.sort;
  }

  getReports() {
    this.http.get<Daemons[]>(this.root_URL + "api/admin/daemons/{1}").subscribe
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
}