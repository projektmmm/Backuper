import { Component, ViewChild, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { BackupReport} from './backup-report';
import { DataTableResource } from 'angular5-data-table';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'backup-reports',
  templateUrl: './backup-reports.component.html',
  styleUrls: ['./backup-reports.component.css']
})
export class BackupReportsComponent implements OnInit {

  constructor(private http: HttpClient) {
    this.getReports();
   }

  ngOnInit() {
  }

  readonly Root_URL = 'http://localhost:54736'; 
  displayedColumns = ['Type', 'Date', 'Size']
  tableResource: MatTableDataSource<BackupReport>;
  items: BackupReport[] = [];
  itemCount: number; 

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  async getReports() {
    this.http.get<BackupReport[]>(this.Root_URL + "/api/admin").subscribe
    (data => { 

      this.inititalizeTable(data);

    });
  }

  private inititalizeTable(data: BackupReport[]) {

    this.tableResource = new MatTableDataSource(data);
    //this.tableResource.query({ offset: 0 })
      //.then(items => this.items = items);

    //this.tableResource.count()
      //.then(count => this.itemCount = count)

      this.tableResource.paginator = this.paginator;
      this.tableResource.sort = this.sort;
  }

  //reloadItems(params) {
    //if (!this.tableResource) return;
    //this.tableResource.query(params)
      //.then(items => this.items = items);

  
}

