import { rowIdService } from './../daemons-info/service';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { Settings } from './settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import { CronLabelComponent } from '../settings-components/cron-label/cron-label.component';
import { MatSnackBar, MAT_DIALOG_DATA } from '@angular/material';
import { query } from '@angular/core/src/animation/dsl';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'send-new-settings',
  templateUrl: './send-new-settings.component.html',
  styleUrls: ['./send-new-settings.component.css']
})
export class SendNewSettingsComponent implements OnInit {
  
  constructor(private http2: HttpClient, public snackBar: MatSnackBar, @Inject(MAT_DIALOG_DATA) public data: any, private rowIdService: rowIdService) {

    if (data != null) 
    {
      this.i_backupType = data.backupType;
      this.i_cron = data.cron;
      this.i_destinationPath = data.destinationPath;
      this.i_sourcePath = data.sourcePath;
      this.i_backupId = data.backupId;
      this.isInEdit = true;
      this.NewOrEdit = data.NewOrEdit;
      /*      
      this.i_serverAdress = data.serverAdress;
      this.i_portNumber = data.portNumber;
      this.i_username = data.username;
      this.i_password = data.passord;*/
    }

    this.headers.append("Content-Type", "application/json");
    this.headers.append("Accept", "application/json");
    this.daemonId = this.rowIdService.rowId;
   }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 1500,
    });
  }

  Fullbackups = [
    {value: 'xxx',viewValue: 'xxx'},
    {value: 'xxx',viewValue: 'xxx'},
    {value: 'xxx',viewValue: 'xxx'}]

  backups = [
    {value: 'FULL', viewValue: 'FULL'},
    {value: 'DIFF', viewValue: 'DIFF'},
    {value: 'INCR', viewValue: 'INCR'}
  ];
  i_backupType: string = "";
  i_cron: string = "";
  i_destinationPath: string = "";
  i_sourcePath: string = "";
  i_serverAdress: string = "";
  i_portNumber: string = "";
  i_username: string = "";
  i_password: string = "";
  i_backupId: number;
  isInEdit: boolean = false;
  daemonId: number;

  isSourceDisabled: boolean = false;

  SourcePathFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^[a-zA-Z]{1}:(.)+"),
  ]);

  DestinationPathFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern("^[a-zA-Z]{1}:(.)+"),
  ]);


  NewOrEdit: string = "New";

  ngOnInit() {
    if(this.NewOrEdit != "New")
    {
      this.initializePaths();
    }
    else
    {
      this.initializeNewPaths();
    }
  }

  headers = new HttpHeaders();
  readonly root_URL = 'http://localhost:63324';

  Send(cron, backupType, ftpserveradress, ftpport, ftpusername, ftppassword, sshserveradress, sshport, sshusername, sshpassword) {

    const head = {headers: new HttpHeaders({'Content-Type':'application/json'})};
    head.headers.append("Authorization", "Bearer " + localStorage.getItem("Token"));
    head.headers.append('Content-Type', 'application/json');

    const data: Settings = {
      DaemonId: this.daemonId,
      Cron: cron,
      BackupType: backupType,
      SourcePath: this.SourcePaths.join(),
      DestinationPath: this.DestinationPaths.join(),
      Id: null,
      FTPServerAdress: ftpserveradress,
      FTPPort: ftpport,
      FTPUsername: ftpusername,
      FTPPassword: ftppassword,
      SSHServerAdress: sshserveradress,
      SSHPort: sshport,
      SSHUsername: sshusername,
      SSHPassword: sshpassword
    }

    if(backupType == "" || cron == "" || this.DestinationPaths.join() == "")
    {
      console.log("xxx");
      this.openSnackBar("Settings not sended","")
    }
    else
    {
      this.http2.post(this.root_URL + "/api/admin/daemon-settings", JSON.stringify(data), {headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
          .subscribe(Response => { console.log(Response) })

      this.openSnackBar("","Settings sended!")
    }
  }

  UpdatePlannedBackup(cron, backupType, ftpserveradress, ftpport, ftpusername, ftppassword, sshserveradress, sshport, sshusername, sshpassword) {
    this

    const head = {headers: new HttpHeaders({'Content-Type':'application/json'})};
    head.headers.append('Content-Type', 'application/json');

    const data: Settings = {
      DaemonId: this.daemonId,
      Cron: cron,
      BackupType: backupType,
      SourcePath: this.SourcePaths.join(),
      DestinationPath: this.DestinationPaths.join(),
      Id: this.i_backupId,
      FTPServerAdress: ftpserveradress,
      FTPPort: ftpport,
      FTPUsername: ftpusername,
      FTPPassword: ftppassword,
      SSHServerAdress: sshserveradress,
      SSHPort: sshport,
      SSHUsername: sshusername,
      SSHPassword: sshpassword
    }

    if(backupType == "" || cron == "")
    {
      this.openSnackBar("Settings not Updated","")
    }
    else
    {
      this.http2.patch(this.root_URL + "/api/admin/planned-backups/" + this.i_backupId, JSON.stringify(data), {headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
          .subscribe(Response => { 
            if (Response == false) {
              this.openSnackBar("Settings not Updated","");
              return;
            }

           })

      this.openSnackBar("","Successfully Updated!")
    }
  }

  initializePaths()
  {
    this.DestinationPaths = this.i_destinationPath.split(",");
    this.SourcePaths = this.i_sourcePath.split(",");
  }

  initializeNewPaths()
  {
    this.DestinationPaths = new Array();
    this.SourcePaths = new Array();
  }

  
  DestinationPaths: string[];
  SourcePaths: string[];

  AddDestinationPath(ipath)
  {
    if(ipath.value != "")
    {
      console.log("destinationpath added")
      this.DestinationPaths.push(ipath.value)
      ipath.value = "";
    }
  }

  AddSourcePath(ipath)
  {
    if(ipath.value != "")
    {
      console.log("sourcepath added")
      this.SourcePaths.push(ipath.value)
      ipath.value = "";
    }   
  }

  DeleteSourcePath(ipath)
  {
    let NewSourcePaths: string[];

    this.SourcePaths

    NewSourcePaths = this.SourcePaths.join().replace(ipath.value,"*").split(",");

    NewSourcePaths = NewSourcePaths.join().replace("*,","").replace(",*","").split(",");

    if(NewSourcePaths.join() == "*")
    {
      NewSourcePaths.pop();
    }

    this.SourcePaths = NewSourcePaths;
  }

  DeleteDestinationPath(ipath)
  {
    let NewDestinationPaths: string[];

    this.SourcePaths

    NewDestinationPaths = this.DestinationPaths.join().replace(ipath.value,"*").split(",");

    NewDestinationPaths = NewDestinationPaths.join().replace("*,","").replace(",*","").split(",");

    if(NewDestinationPaths.join() == "*")
    {
      NewDestinationPaths.pop();
    }

    this.DestinationPaths = NewDestinationPaths;
  }

  DisableSources(iBackupType,iNewSourcePath)
  {
    if(iBackupType.value != "FULL")
    {
      this.isSourceDisabled = true;
    }
    else
    {
      this.isSourceDisabled = false;
    }
  }

  OnTabChanges(currentTabIndex)
  {
    //console.log("sss")
    //localStorage.setItem("ActiveTab",currentTabIndex)
  }

  UpdateCron(Cron,TimeTab)
  {
    
  }

  UpdateCronMinutes(EMMinutes, Cron)
  {
    if(EMMinutes.value >= 1 && EMMinutes.value <= 59)
    {
      Cron.value = "*/" + EMMinutes.value + " * * * *";
      //cron updated
    }
    else
    {    
      this.openSnackBar("Enter number between 1 and 59","");
    }
  }


  changeLocalCheckBox(LocalCheckbox,FTPCheckbox,SSHCheckbox)
  {
    if(FTPCheckbox.checked)
    {
      LocalCheckbox.checked = true;
      FTPCheckbox.checked = false;
      SSHCheckbox.checked = false;
    }
    else
    {
      LocalCheckbox.checked = true;
      FTPCheckbox.checked = false;
      SSHCheckbox.checked = false;
    }
  }

  changeFTPCheckBox(LocalCheckbox,FTPCheckbox,SSHCheckbox)
  {
    if(LocalCheckbox.checked)
    {
      LocalCheckbox.checked = false;
      FTPCheckbox.checked = true;
      SSHCheckbox.checked = false;
    }
    else
    {
      LocalCheckbox.checked = false;
      FTPCheckbox.checked = true;
      SSHCheckbox.checked = false;
    }
  }

  changeSSHCheckBox(LocalCheckbox,FTPCheckbox,SSHCheckbox)
  {
    if(LocalCheckbox.checked)
    {
      LocalCheckbox.checked = false;
      FTPCheckbox.checked = false;
      SSHCheckbox.checked = true;
    }
    else
    {
      LocalCheckbox.checked = false;
      FTPCheckbox.checked = false;
      SSHCheckbox.checked = true;
    }
  }

  UpdateCronHourly(Cron, EveryHourRadioHourly, StartEveryHourRadioHourly, EveryHourHourly, StartEveryHourHourly, StartEveryMinuteHourly)
  {
    if(EveryHourRadioHourly.checked)
    {
      if(EveryHourHourly.value >= 1 && EveryHourHourly.value <= 23)
      {
        Cron.value = "* */" + EveryHourHourly.value + " * * *";
        //cron updated
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
        //cron updated
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
        //cron updated
      }
      else
      {
        this.openSnackBar("Enter number between 1 and 31","");
      }
      
    }
    else
    {
      Cron.value = StartMinutesDaily.value + " " + StartHoursDaily.value + " * * *";
      //cron updated
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
      //cron updated
    }
    else
    {
      this.openSnackBar("Choose on or more days","");
    }
  }

  
}
