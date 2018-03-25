import { Component, OnInit } from '@angular/core';
import { Settings } from './settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { MatSlider } from '@angular/material/slider';

@Component({
  selector: 'daemons',
  templateUrl: './daemons.component.html',
  styleUrls: ['./daemons.component.css']
})
export class DaemonsComponent implements OnInit {

  constructor(private http2: HttpClient) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
   }

  ngOnInit() {
  }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:54736';

  Send(daemonId, runAt, cron, backupType, sourcePath, destinationPath) {

    const head = {headers: new HttpHeaders({'Content-Type':'application/json'})};
    head.headers.append('Content-Type', 'application/json');

    const data: Settings = {
      DaemonId: daemonId,
      RunAt: new Date,
      Cron: cron,
      BackupType: backupType,
      SourcePath: sourcePath,
      DestinationPath: destinationPath
    }

    this.http2.post(this.root_URL + "/api/admin/form", JSON.stringify(data), head)
    .subscribe(Response => { console.log(Response) })

  }
}
