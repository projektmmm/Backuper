import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {RouterModule} from '@angular/router'
import {DataTableModule} from 'angular5-data-table';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';


import {AppComponent} from './app.component';
import {BsNavbarComponent} from './bs-navbar/bs-navbar.component';
import {BackupReportsComponent} from './backup-reports/backup-reports.component';
import {DaemonsComponent} from './daemons/daemons.component';
import {AdminSettingsComponent} from './admin-settings/admin-settings.component';
import {HomeComponent} from './home/home.component';

import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatMenuModule} from '@angular/material/menu';
import {MatInputModule} from '@angular/material/input';
import {MatTableModule} from '@angular/material/table';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTabsModule} from '@angular/material/tabs';
import {MatSliderModule} from '@angular/material/slider';
import { MatPaginatorModule } from '@angular/material';

import 'hammerjs';


@NgModule({
  declarations: [
    AppComponent,
    BsNavbarComponent,
    BackupReportsComponent,
    DaemonsComponent,
    AdminSettingsComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    DataTableModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      {path: '', component: HomeComponent},
      {path: 'admin-settings', component: AdminSettingsComponent},
      {path: 'backup-reports', component: BackupReportsComponent},
      {path: 'daemons', component: DaemonsComponent}
    ]),
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatInputModule,
    MatTableModule,
    MatGridListModule,
    MatTabsModule,
    MatSliderModule,
    MatPaginatorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
