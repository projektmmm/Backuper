import { Component, ViewChild, OnInit, Input } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { Backups} from './backups';
import { DataTableResource } from 'angular5-data-table';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import {MatPaginatorModule} from '@angular/material/paginator';

@Component({
  selector: 'planned-backups',
  templateUrl: './planned-backups.component.html',
  styleUrls: ['./planned-backups.component.css']
})
export class PlannedBackupsComponent implements OnInit {

  constructor(private http: HttpClient) {
    this.getBackup();
   }

  ngOnInit() {
  }

  readonly Root_URL = 'http://localhost:54736'; 
  displayedColumns = ['RunAt', 'Cron', 'DaemonId', 'BackupType', 'SourcePath', 'DestinationPath', 'Edit'];
  tableResource: MatTableDataSource<Backups>;
  items: Backups[] = [];
  itemCount: number;
  showVar: boolean;
  rowIndex: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  async getBackup() {
    this.http.get<Backups[]>(this.Root_URL + "/api/admin/backups").subscribe
    (data => { 

      this.inititalizeTable(data);

    });
  }

  private inititalizeTable(data: Backups[]) {

    this.tableResource = new MatTableDataSource(data);
    //this.tableResource.query({ offset: 0 })
      //.then(items => this.items = items);

    //this.tableResource.count()
      //.then(count => this.itemCount = count)

      this.tableResource.paginator = this.paginator;
      this.tableResource.sort = this.sort;
  }

  show($event) {
    this.showVar = !this.showVar;
    console.log("erroadfr");
  }

  //reloadItems(params) {
    //if (!this.tableResource) return;
    //this.tableResource.query(params)
      //.then(items => this.items = items);

  

}
