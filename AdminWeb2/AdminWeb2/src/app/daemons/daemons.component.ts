import { Component, OnInit } from '@angular/core';
import { Settings } from './settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { CronLabelComponent } from '../settings-components/cron-label/cron-label.component';
import {MatSnackBar} from '@angular/material';
import { query } from '@angular/core/src/animation/dsl';

@Component({
  selector: 'daemons',
  templateUrl: './daemons.component.html',
  styleUrls: ['./daemons.component.css']
})
export class DaemonsComponent implements OnInit {

  constructor(private http2: HttpClient, public snackBar: MatSnackBar) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
   }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    });
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

    this.openSnackBar("Settings sended!","")
  }

  UpdateCronMinutes(EMMinutes, Cron)
  {
    if(EMMinutes.value >= 1 && EMMinutes.value <= 59)
    {
      Cron.value = "*/" + EMMinutes.value + " * * * *";
    }
    else
    {    
      this.openSnackBar("Enter number between 1 and 59","");
    }
  }

  UpdateCronHourly(Cron, EveryHourRadioHourly, StartEveryHourRadioHourly, EveryHourHourly, StartEveryHourHourly, StartEveryMinuteHourly)
  {
    if(EveryHourRadioHourly.checked)
    {
      if(EveryHourHourly.value >= 1 && EveryHourHourly.value <= 23)
      {
        Cron.value = "* */" + EveryHourHourly.value + " * * *";
      }
      else
      {
        this.openSnackBar("Enter number between 1 and 23","");
      }
    }
    else
    {
      if((StartEveryHourHourly.value >= 0 && StartEveryHourHourly.value <= 23) && (StartEveryMinuteHourly.value >= 0 && StartEveryMinuteHourly.value <= 59))
      {
        if(StartEveryMinuteHourly.value == 0)
        {
          StartEveryMinuteHourly.value = 0;   
        }
        
        if(StartEveryHourHourly.value == 0)
        {
          StartEveryHourHourly.value = 0;
        }

        Cron.value = StartEveryMinuteHourly.value + " " + StartEveryHourHourly.value + " * * *";
      }
      else
      {
        this.openSnackBar("Wrong time format","");
      }   
    }
  }

  UpdateCronDaily(Cron, RadioOneDaily, RadioTwoDaily, DaysOneDaily, StartHoursDaily, StartMinutesDaily)
  {
    if(RadioOneDaily.checked)
    {
      if(DaysOneDaily.value >= 1 && DaysOneDaily.value <= 31)
      {
        if(StartMinutesDaily.value == 0)
        {
          StartMinutesDaily.value = 0;
        }

        if(StartHoursDaily.value == 0)
        {
          StartHoursDaily.value = 0;
        }

        Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " */" + DaysOneDaily.value + " * *";
      }
      else
      {
        this.openSnackBar("Enter number between 1 and 31","");
      }
      
    }
    else
    {
      Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " * * *";
    }
  }

  UpdateCronWeekly(Cron,CheckMonday, CheckTuesday, CheckWednesday, CheckThursday, CheckFriday, CheckSaturday,CheckSunday,StartHoursWeekly,StartMinutesWeekly){
 
 
    let result: string = "";
    if(CheckMonday.checked){
      result= result + "1,"
    }

    if(CheckTuesday.checked){
      result= result + "2,"
    }

    if(CheckWednesday.checked){
      result= result + "3,"
    }

    if(CheckThursday.checked){
      result= result + "4,"
    }

    if(CheckFriday.checked){
      result= result + "5,"
    }

    if(CheckSaturday.checked){
      result= result + "6,"
    }

    if(CheckSunday.checked){
      result= result + "0,"
    }

    result = result.slice(0, -1)

    if(result != "")
    {
      if(StartHoursWeekly.value == 0)
      {
        StartHoursWeekly.value = 0;
      }

      if(StartMinutesWeekly.value == 0)
      {
        StartMinutesWeekly.value = 0;
      }

      Cron.value = StartMinutesWeekly.value + " " + StartHoursWeekly.value + " * * " + result
    }
    else
    {
      this.openSnackBar("Choose on or more days","");
    }
  }
}
