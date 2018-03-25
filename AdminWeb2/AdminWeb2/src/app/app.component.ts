import { Component } from '@angular/core';
import { Router} from '@angular/router';
import { RouterLink, Route } from '@angular/router';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
}
