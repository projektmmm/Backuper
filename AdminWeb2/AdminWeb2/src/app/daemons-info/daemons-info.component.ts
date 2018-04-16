import { Daemons } from './../daemons/daemons';
import { rowIdService } from './service';
import { ErrorInfoComponent } from './error-info/error-info.component';
import { SendSettingsComponent } from './../settings-components/send-settings/send-settings.component';
import { Component, OnInit, Inject, Output } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Backups, ErrorDetails } from './interfaces';
import { DataSource } from '@angular/cdk/table';
import { DataTableResource } from 'angular5-data-table';
import { FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'daemons-info',
  templateUrl: './daemons-info.component.html',
  styleUrls: ['./daemons-info.component.css']
})
export class DaemonsInfoComponent implements OnInit {

  constructor(private http: HttpClient, public dialog: MatDialog, private rowIdService: rowIdService) {
    this.daemonId = this.rowIdService.rowId;
    this.showBackupReports = true;
    this.getDaemonInfo();
    this.getBackups();
    this.getErrors();
   }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  plannedBackups: Backups[];
  errorDetails: ErrorDetails[];
  warningButtonText: string = "";
  hideWarningButton: boolean = true;
  editSettings: boolean = true;
  warningButtonColor: string;
  warningButtonIcon: string;
  showBackupReports: boolean = false;
  daemonName: string;
  daemonDescription: string;
  @Output() daemonId: number;

  ngOnInit() {
  }


  newBackup() {
    const dialogRef = this.dialog.open(SendSettingsComponent, {
      
    });

    dialogRef.afterClosed().subscribe(
      result => {
        console.log(result);
      }
    )
  }


  getNextBackup() {
    /*
    this.plannedBackups.forEach(element => {
      //TODO
      //if (element.)
    }); */
  }

  getErrors() {
    this.http.get<ErrorDetails[]>(this.root_URL + "/api/admin/backup-errors/" + localStorage.getItem("Username") + "-" + this.daemonId).subscribe
    (data => {

      this.errorDetails = data;
      this.checkErrors();
    })
  }

  private checkErrors() {
    if (this.errorDetails[0].Problem == "N" || this.errorDetails[0].Solved == true) {

      this.warningButtonText = "No problems";
      this.warningButtonColor = "primary";
      this.warningButtonIcon = "check_circle";
    }
    else {

    this.warningButtonText = "Problems";
    this.warningButtonColor = "warn";
    this.warningButtonIcon = "error_outline";
    }

    this.hideWarningButton = false;
  }

  async getBackups() {
    this.http.get<Backups[]>(this.root_URL + "/api/admin/planned-backups").subscribe
    (data => { 
      this.plannedBackups = data;
    });
  }

  async getDaemonInfo() {
    this.http.get<Daemons>(this.root_URL + "/api/admin/daemons/" + localStorage.getItem("Username") + "-" + this.daemonId).subscribe
    (data => {
      this.daemonDescription = data.Description;
      this.daemonName = data.Name;
    })
  }

  cancelFormEditing() {

    this.editSettings = true;
    this.getDaemonInfo();
  }

  saveFormEditing(name: string, description: string) { 
    console.log(name);
    console.log(description);
  }

  showWarning() {
    
      let dialogRef = this.dialog.open(ErrorInfoComponent, {
        width: '700px',
        //height: '600px',
        data: { daemonId: this.daemonId}
      });

      dialogRef.afterClosed().subscribe(result => {
        this.getErrors();
      });    
  }



  allowEdit() {
    this.editSettings = false;
  }



}
