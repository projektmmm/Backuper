import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { PlannedBackupsComponent } from '../planned-backups.component';
import { UNIQUE_SELECTION_DISPATCHER_PROVIDER } from '@angular/cdk/collections';

@Component({
  selector: 'app-paths',
  templateUrl: './paths.component.html',
  styleUrls: ['./paths.component.css']
})
export class PathsComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<PlannedBackupsComponent>, public snackBar: MatSnackBar, @Inject(MAT_DIALOG_DATA) public data: any) 
  { 
    if (data != null) {
      this.path = data.path;
      this.pathtype = data.pathtype;
    }
  }

  

  ngOnInit() {
    //let paths: string[];
    //paths = this.path.split(',');
    this.paths = this.path.split(",");
  }

  pathtype: string;
  paths: string[];
  path: string;

  closeDialog() {
    this.dialogRef.close()
  }

}
