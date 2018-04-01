import { MatSnackBar } from '@angular/material';
import { Component, OnInit, Input } from '@angular/core';
import { Settings } from './settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';

@Component({
  selector: 'send-settings',
  templateUrl: './send-settings.component.html',
  styleUrls: ['./send-settings.component.css']
})
export class SendSettingsComponent implements OnInit {

  
  constructor(private http2: HttpClient, public snackBar: MatSnackBar) {
    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
   }

  ngOnInit() {
  }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';
  @Input() show: boolean;
  @Input() id: number;
  @Input() runAt: Date;
  @Input() cron: string;
  @Input() daemonId: number;
  @Input() backupType: string;
  @Input() sourcePath: string;
  @Input() destinationPath: string;

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    }); 
  }

  Update(daemonId, runAt, cron, backupType, sourcePath, destinationPath) {

    const head = {headers: new HttpHeaders({'Content-Type':'application/json'})};
    head.headers.append('Content-Type', 'application/json');

    const data: Settings = {
      Id: this.id,
      DaemonId: daemonId,
      RunAt: new Date,
      Cron: cron,
      BackupType: backupType,
      SourcePath: sourcePath,
      DestinationPath: destinationPath
    }

    this.http2.patch(this.root_URL + "/api/admin/planned-backups/" + this.id, JSON.stringify(data), head)
    .subscribe(response => { 
      
      if (response == true) {
      this.show = !this.show;
      this.openSnackBar("", "Success!")
      }
      else {
        this.openSnackBar("", "Error!");
      }

    })

  }

  UpdateCronMinutes(EMMinutes, Cron){
    if(EMMinutes.value > 59 || EMMinutes.value < 1){
      alert("enter number between 1 and 59")
    }
    else{
      if(EMMinutes.value == ""){
      Cron.value = "";
      }
      else {
      Cron.value = "*/" + EMMinutes.value + " * * * *";
      }
    }
  }

  UpdateCronHourly(Cron, EveryHourRadioHourly, StartEveryHourRadioHourly, EveryHourHourly, StartEveryHourHourly, StartEveryMinuteHourly){
    if(EveryHourRadioHourly.checked){
      Cron.value = "* */" + EveryHourHourly.value + " * * *";
    }
    else{
      Cron.value = StartEveryMinuteHourly.value + " " + StartEveryHourHourly.value + " * * *"
    }
  }

  UpdateCronDaily(Cron, RadioOneDaily, RadioTwoDaily, DaysOneDaily, StartHoursDaily, StartMinutesDaily){
    if(RadioOneDaily.checked){
      Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " */" + DaysOneDaily.value + " * *" 
    }
    else{
      Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " * * *"
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

    if(result == "" || StartHoursWeekly.value == "" || StartMinutesWeekly.value == ""){

    }
    else{
      Cron.value = StartMinutesWeekly.value + " " + StartHoursWeekly.value + " * * " + result
    }
  }

}
