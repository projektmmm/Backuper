import {HttpClientModule} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router'
import {Route, RouterLink} from '@angular/router'
import {DataTableModule} from 'angular5-data-table';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';


import {AppComponent} from './app.component';
import {BsNavbarComponent} from './bs-navbar/bs-navbar.component';
import {BackupReportsComponent} from './backup-reports/backup-reports.component';
import {DaemonsComponent} from './daemons/daemons.component';
import {AdminSettingsComponent} from './admin-settings/admin-settings.component';
import {HomeComponent} from './home/home.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';


import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatMenuModule} from '@angular/material/menu';
import {MatInputModule} from '@angular/material/input';
import {MatTableModule} from '@angular/material/table';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTabsModule} from '@angular/material/tabs';
import {MatSliderModule} from '@angular/material/slider';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatRadioModule} from '@angular/material/radio';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';


import 'hammerjs';
import { PlannedBackupsComponent } from './planned-backups/planned-backups.component';
import { CronLabelComponent } from './settings-components/cron-label/cron-label.component';
import { SendSettingsComponent} from './settings-components/send-settings/send-settings.component';
import { MatSortModule } from '@angular/material';



@NgModule({
  declarations: [
    AppComponent,
    BsNavbarComponent,
    BackupReportsComponent,
    DaemonsComponent,
    AdminSettingsComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    PlannedBackupsComponent,
    CronLabelComponent,
    SendSettingsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    DataTableModule,
    RouterModule.forRoot([
      {path: '', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'home', component: HomeComponent},
      {path: 'admin-settings', component: AdminSettingsComponent},
      {path: 'backup-reports', component: BackupReportsComponent},
      {path: 'daemons', component: DaemonsComponent},
      {path: 'login', component: LoginComponent},
      {path: 'planned-backups', component: PlannedBackupsComponent}
    ]),
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatInputModule,
    MatTableModule,
    MatGridListModule,
    MatTabsModule,
    MatSliderModule,
    MatPaginatorModule,
    MatRadioModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatSortModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
