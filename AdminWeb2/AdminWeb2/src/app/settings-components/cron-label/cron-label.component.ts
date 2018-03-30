import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'cron-label',
  templateUrl: './cron-label.component.html',
  styleUrls: ['./cron-label.component.css']
})
export class CronLabelComponent implements OnInit {

  constructor() { }

  ngOnInit() {
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
