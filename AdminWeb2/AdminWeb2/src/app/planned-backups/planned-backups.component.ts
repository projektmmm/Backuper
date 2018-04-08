import { Component, ViewChild, OnInit, Input, Output } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { Backups} from './backups';
import { DataTableResource } from 'angular5-data-table';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'planned-backups',
  templateUrl: './planned-backups.component.html',
  styleUrls: ['./planned-backups.component.css']
})
export class PlannedBackupsComponent implements OnInit {

  constructor(private http: HttpClient, public snackBar: MatSnackBar) {
    this.getBackup();
   }

  ngOnInit() {
  }

  readonly Root_URL = 'http://localhost:63324'; 
  displayedColumns = ['NextRun', 'Cron', 'DaemonId', 'BackupType', 'SourcePath', 'DestinationPath', 'Buttons'];
  tableResource: MatTableDataSource<Backups>;
  items: Backups[] = [];
  itemCount: number;
  showVar: boolean = false;
  rowIndex: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @Output() id: number;
  @Output() nextRun: Date;
  @Output() cron: string;
  @Output() daemonId: number;
  @Output() backupType: string;
  @Output() sourcePath: string;
  @Output() destinationPath: string;
  
  ngAfterViewInit() {
    if (this.tableResource != null)
      this.tableResource.sort = this.sort;
  }

  async getBackup() {
    this.http.get<Backups[]>(this.Root_URL + "/api/admin/planned-backups").subscribe
    (data => { 

      this.inititalizeTable(data);
      this.tableResource.sort = this.sort;
      
    });
  }

  
  async delete(row) {
    this.http.delete(this.Root_URL + "/api/admin/planned-backups/" + row.Id).subscribe
    (response => {
     
      if (response == true) {
        this.openSnackBar("", "Successfully deleted!");
        this.getBackup();
      }
      else {
        this.openSnackBar("", "Error!")
        this.getBackup();
      }

    })

  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
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
    this.nextRun = row.nextRun;
    this.cron = row.Cron;
    this.daemonId = row.DaemonId;
    this.backupType = row.BackupType;
    this.sourcePath = row.SourcePath;
    this.destinationPath = row.DestinationPath;
    this.id = row.Id;
  }


  

}
