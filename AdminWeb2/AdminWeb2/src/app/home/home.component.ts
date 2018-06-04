import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { HttpObserve } from '@angular/common/http/src/client';
import { Backups, ErrorDetails } from './interfaces';
import {MatMenuModule} from '@angular/material/menu';
import { DataSource } from '@angular/cdk/table';
import { RouterLink, Router,ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient,
    private route: ActivatedRoute,
    private router:Router) { }

  Username: string = localStorage.getItem("Username")
  DataSource: ErrorDetails[];
  readonly root_URL = 'http://localhost:63324/';
  Temp: number 
  readonly weatherUrl = 'http://api.openweathermap.org/data/2.5/weather?q=Prague&APPID=ec047c56454129612a6f3da4e8b38bfa'
  Datum: string = new Date().toLocaleDateString();
  Weather
  WeatherIcon
  WeatherDesc : string;
  ShowProblems: boolean = true;

  ngOnInit() {
    this.http.get<ErrorDetails[]>(this.root_URL + "api/admin/Home/"+ this.Username, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))}).subscribe
    (data => {
      this.DataSource = data;
      if (data.length == 0)
      {
        this.ShowProblems = false;
      }
    })
    
    this.http.get<string>(this.weatherUrl).subscribe
    (Data => {
      this.Weather = Data;
      this.Temp =this.Weather["main"]["temp"]-273.15
      this.Temp = Math.round(this.Temp)
      this.WeatherIcon = this.Weather["weather"][0]["icon"]
      this.WeatherIcon = this.http.get("http://openweathermap.org/img/w/"+this.WeatherIcon+".png")
      console.log(this.WeatherIcon)
      this.WeatherDesc = this.Weather["weather"][0]["description"]
    })
  }

  ToDaemon(ReportId:number) {
    this.http.get<number>(this.root_URL+"api/admin/Home/Daemon/"+ ReportId, {headers: new HttpHeaders().set("Authorization", "Bearer " + localStorage.getItem("Token"))}).subscribe
    (data => {
      this.router.navigate(["./daemons-info/"+data])
    })
  }

}