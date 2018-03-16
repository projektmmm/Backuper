import { Component, OnInit } from '@angular/core';
import { settings } from 'cluster';
import {Settings} from './Settings';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import { NullTemplateVisitor } from '@angular/compiler';
import { post } from 'selenium-webdriver/http';
@Component({
  selector: 'app-deamons-settings',
  templateUrl: './deamons-settings.component.html',
  styleUrls: ['./deamons-settings.component.css']
})
export class DeamonsSettingsComponent implements OnInit {

  constructor(private http2: HttpClient) {}
  readonly Root_URL = 'http://localhost:54736';

  ngOnInit() {
  }
  Data: Settings;
  Send(){
    /*this.Data.DeamonId = 1;
    this.Data.RunAt = new Date;
    this.Data.Cron = "Macek";
    this.Data.BackupType = "The";
    this.Data.SourcePath = "Angular";
    this.Data.DestinationPath = "Master";
    */
   const data: Settings ={
    DeamonId: 1,
    RunAt: new Date ,
    Cron: "Macek",
    BackupType: "The",
    SourcePath: "Angular",
    DestinationPath: "Master"
  }
 
    this.http2.post<string>(this.Root_URL+"/api/admin",JSON.stringify(data))
    .subscribe(Response=>{console.log(JSON.stringify(Response))
    })
    /*console.log(JSON.stringify(data))*/
}}
