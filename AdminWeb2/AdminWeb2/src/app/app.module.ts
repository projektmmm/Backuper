import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RouterModule} from '@angular/router'


import { AppComponent } from './app.component';
import { BsNavbarComponent } from './bs-navbar/bs-navbar.component';
import { BackupReportsComponent } from './backup-reports/backup-reports.component';
import { DaemonsComponent } from './daemons/daemons.component';
import { AdminSettingsComponent } from './admin-settings/admin-settings.component';
import { HomeComponent } from './home/home.component';


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
    HttpClientModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent},
      { path: 'admin-settings', component: AdminSettingsComponent},
      { path: 'backup-reports', component: BackupReportsComponent},
      { path: 'daemons', component: DaemonsComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
