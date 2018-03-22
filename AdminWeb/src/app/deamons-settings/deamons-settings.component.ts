import { Component, OnInit } from '@angular/core';
import {Settings} from './Settings';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import {DamonPokus} from './DamonPokus'
import { Http, Headers } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import { RequestOptions } from '@angular/http';



@Component({
  selector: 'app-deamons-settings',
  templateUrl: './deamons-settings.component.html',
  styleUrls: ['./deamons-settings.component.css']
})
export class DeamonsSettingsComponent implements OnInit {
  headers = new HttpHeaders();
  constructor(private http2: HttpClient) {
    this.headers.append("Content-Type","application/json");
    this.headers.append('Accept', 'application/json',)
    
  }
  readonly Root_URL = 'http://localhost:54736';
  
  ngOnInit() {
  }
 
  Send(DeamonId,RunAt,Cron,BackupType,SorcePath,DestinationPath){
    
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    const data: Settings ={
      DeamonId: DeamonId,
      RunAt: new Date ,
      Cron: Cron,
      BackupType: BackupType,
      SourcePath: SorcePath,
      DestinationPath: DestinationPath
    } 
     this.http2.post(this.Root_URL+"/api/admin/form",JSON.stringify(data),head)
     .subscribe(Response=>{console.log(Response)
     })
  }

  Edit(Cron){
    Cron.value = "xxxx"
  }
}
