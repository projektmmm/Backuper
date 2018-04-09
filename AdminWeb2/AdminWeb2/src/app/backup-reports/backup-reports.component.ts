import { Component, ViewChild, OnInit, Input } from '@angular/core';
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

  readonly Root_URL = 'http://localhost:63324/'; 
  displayedColumns = ['Type', 'Date', 'Size']
  tableResource: MatTableDataSource<BackupReport>;
  items: BackupReport[] = [];
  itemCount: number; 
  adress: string = this.Root_URL + "/api/admin/backup-reports";

  @Input() daemonId: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  getReports() {
    if(this.daemonId != null)
      this.adress = this.adress + "/" + localStorage.getItem("Username") + "-" + this.daemonId;
    this.http.get<BackupReport[]>(this.adress).subscribe
    (data => { 

      this.inititalizeTable(data);
    });
  }

  ngAfterViewInit() {
    if (this.tableResource != null)
      this.tableResource.sort = this.sort;
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

