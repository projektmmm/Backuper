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
    public dialogRef: MatDialogRef<DaemonsInfoComponent>, @Inject(MAT_DIALOG_DATA) public data: ErrorDetails[]) { 
      this.errorDetails = data;
    }

  ngOnInit() {
  }

  errorDetails: ErrorDetails[];
}
