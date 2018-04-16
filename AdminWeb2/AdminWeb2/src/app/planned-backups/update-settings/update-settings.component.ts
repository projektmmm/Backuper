import { Component, OnInit } from '@angular/core';
import { SendSettingsComponent } from '../../settings-components/send-settings/send-settings.component'
import { MatSnackBar } from '@angular/material';
import { Input } from '@angular/core';
import { Settings } from '../../settings-components/send-settings/settings';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions} from '@angular/http';
import {FormControl, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';



@Component({
  selector: 'update-settings',
  templateUrl: './update-settings.component.html',
  styleUrls: ['./update-settings.component.css']
})
export class UpdateSettingsComponent implements OnInit {

  constructor() {
   }

  ngOnInit() {
  } 


}
