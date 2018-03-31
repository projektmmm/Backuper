import { Component, ViewChild, OnInit, Input, Output } from '@angular/core';
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
  displayedColumns = ['RunAt', 'Cron', 'DaemonId', 'BackupType', 'SourcePath', 'DestinationPath', 'Buttons'];
  tableResource: MatTableDataSource<Backups>;
  items: Backups[] = [];
  itemCount: number;
  showVar: boolean = false;
  rowIndex: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @Output() runAt: Date;
  @Output() cron: string;
  @Output() daemonId: number;
  @Output() backupType: string;
  @Output() sourcePath: string;
  @Output() destinationPath: string;
  

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

  show(row) {
    this.showVar = !this.showVar;
    this.runAt = row.RunAt;
    this.cron = row.Cron;
    this.daemonId = row.DaemonId;
    this.backupType = row.BackupType;
    this.sourcePath = row.SourcePath;
    this.destinationPath = row.DestinationPath


  }

  async delete(row) {

    //TODO

  }

  

}
