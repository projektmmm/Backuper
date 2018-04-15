import { Component, OnInit } from '@angular/core';
import { Settings } from './settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { CronLabelComponent } from '../settings-components/cron-label/cron-label.component';
import {MatSnackBar} from '@angular/material';
import { query } from '@angular/core/src/animation/dsl';
import {FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'send-new-settings',
  templateUrl: './send-new-settings.component.html',
  styleUrls: ['./send-new-settings.component.css']
})
export class SendNewSettingsComponent implements OnInit {
  
  constructor(private http2: HttpClient, public snackBar: MatSnackBar) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
   }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    });
  }

  backups = [
    {value: 'FULL', viewValue: 'FULL'},
    {value: 'DIFF', viewValue: 'DIFF'},
    {value: 'INCR', viewValue: 'INCR'}
  ];

  SourcePathFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^[a-zA-Z]{1}:(.)+"),
  ]);

  DestinationPathFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^[a-zA-Z]{1}:(.)+"),
  ]);

  SourcePathmatcher = new MyErrorStateMatcher();
  DestinationPathmatcher = new MyErrorStateMatcher();



  ngOnInit() {
  }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';

  Send(daemonId, runAt, cron, backupType, sourcePath, destinationPath) {

    const head = {headers: new HttpHeaders({'Content-Type':'application/json'})};
    head.headers.append('Content-Type', 'application/json');

    const data: Settings = {
      DaemonId: daemonId,
      UserId: 1,
      RunAt: new Date,
      Cron: cron,
      BackupType: backupType,
      SourcePath: sourcePath,
      DestinationPath: destinationPath,
      Id: null
    }

    this.http2.post(this.root_URL + "/api/admin/daemon-settings", JSON.stringify(data), head)
    .subscribe(Response => { console.log(Response) })

    this.openSnackBar("","Settings sended!")
  }

  OnTabChanges(currentTabIndex)
  {
    console.log("sss")
    localStorage.setItem("ActiveTab",currentTabIndex)
  }

  UpdateCron(Cron,TimeTab)
  {
    
  }

  UpdateCronMinutes(EMMinutes, Cron)
  {
    if(EMMinutes.value >= 1 && EMMinutes.value <= 59)
    {
      Cron.value = "*/" + EMMinutes.value + " * * * *";
      this.openSnackBar("","Cron updated!")
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
        this.openSnackBar("","Cron updated!")
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
        this.openSnackBar("","Cron updated!")
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
        this.openSnackBar("","Cron updated!")
      }
      else
      {
        this.openSnackBar("Enter number between 1 and 31","");
      }
      
    }
    else
    {
      Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " * * *";
      this.openSnackBar("","Cron updated!")
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
      this.openSnackBar("","Cron updated!")
    }
    else
    {
      this.openSnackBar("Choose on or more days","");
    }
  }

  
}
