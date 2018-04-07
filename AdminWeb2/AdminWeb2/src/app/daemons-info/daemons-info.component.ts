import { SendSettingsComponent } from './../settings-components/send-settings/send-settings.component';
import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { MatDialog } from '@angular/material';
import { Backups } from './backups';


@Component({
  selector: 'daemons-info',
  templateUrl: './daemons-info.component.html',
  styleUrls: ['./daemons-info.component.css']
})
export class DaemonsInfoComponent implements OnInit {

  constructor(private http: HttpClient, public dialog: MatDialog) {
    this.getBackups();
   }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  plannedBackups: Backups[];

  ngOnInit() {
  }


  newBackup() {
    const dialogRef = this.dialog.open(SendSettingsComponent, {
      height: '600px'
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

  getBackups() {
    this.http.get<Backups[]>(this.root_URL + "/api/admin/planned-backups").subscribe
    (data => { 
      this.plannedBackups = data;
      /*
      this.inititalizeTable(data);
      this.tableResource.sort = this.sort;
      */
    });
  }



}
