import { ErrorDetails } from './../interfaces';
import { DaemonsInfoComponent } from './../daemons-info.component';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'error-info',
  templateUrl: './error-info.component.html',
  styleUrls: ['./error-info.component.css']
})
export class ErrorInfoComponent implements OnInit {

  constructor (
    @Inject(MAT_DIALOG_DATA) public data: any) { 
      //this.errorDetails = data;
      //this.Id = data[0].Id;
      this.Name = data.Name;
      
    }

  ngOnInit() {
  }

  Id: number;
  errorDetails: ErrorDetails[];
  Name: string;
  
}
