import { rowIdService } from './../daemons-info/service';
import { SendNewSettingsComponent } from './../send-new-settings/send-new-settings.component';
import { Component, ViewChild, OnInit, Input, Output } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { Backups} from './backups';
import { DataTableResource } from 'angular5-data-table';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material';
import { UpdateSettingsComponent } from './update-settings/update-settings.component';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SendSettingsComponent } from './../settings-components/send-settings/send-settings.component';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { DataSource } from '@angular/cdk/table';
import { FormGroupDirective } from '@angular/forms';
import { forEach } from '@angular/router/src/utils/collection';


@Component({
  selector: 'planned-backups',
  templateUrl: './planned-backups.component.html',
  styleUrls: ['./planned-backups.component.css']
})
export class PlannedBackupsComponent implements OnInit {

  constructor(private http: HttpClient, public snackBar: MatSnackBar, public dialog: MatDialog, private rowIdService: rowIdService) {
    
   }

  ngOnInit() {
    this.getBackup();
  }

  readonly Root_URL = 'http://localhost:63324'; 
  displayedColumns = ['NextRun', 'Cron', 'BackupType', 'SourcePath', 'DestinationPath', 'Buttons'];
  tableResource: MatTableDataSource<Backups>;
  items: Backups[] = [];
  itemCount: number;
  showVar: boolean = false;
  rowIndex: number;
  username: string = localStorage.getItem("Username");
  adress: string = this.Root_URL + "/api/admin/planned-backups";

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @Output() id: number;
  @Output() nextRun: Date;
  @Output() cron: string;
  @Output() daemonId: number;
  @Output() backupType: string;
  @Output() sourcePath: string;
  @Output() destinationPath: string;
  @Input() actuallDaemonId: number;
  
  ngAfterViewInit() {
    if (this.tableResource != null)
      this.tableResource.sort = this.sort;
  }

  async getBackup() {
    if(this.actuallDaemonId != null) {
    this.rowIdService.rowId = this.actuallDaemonId;
      this.adress = this.adress + "/" + localStorage.getItem("Username") + "-" + this.actuallDaemonId; 
    }
    this.http.get<Backups[]>(this.adress).subscribe
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

    //data.forEach(function (path)
    //{
    //  path.SourcePath = path.SourcePath.replace(",","\n");
    //  
    //  path.DestinationPath = path.DestinationPath.replace(",","\n");
    //});

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


  showUpdateSettings(o_backupType: string, o_cron: string, o_destinationPath: string, o_sourcePath: string, o_backupId: number)
  {
    console.log(o_backupId);
    let dialogRef = this.dialog.open(SendNewSettingsComponent, {
      data: { 
        backupType: o_backupType,
        cron: o_cron,
        destinationPath: o_destinationPath,
        sourcePath: o_sourcePath,
        backupId: o_backupId,
        NewOrEdit: "Edit"
      }
    });

    
  }

}
