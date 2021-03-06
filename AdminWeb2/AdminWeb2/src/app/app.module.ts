import { HttpModule } from '@angular/http';
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
import {MatSelectModule} from '@angular/material/select';
import {MatCardModule} from '@angular/material/card';
import {MatTooltipModule} from '@angular/material/tooltip';

import 'hammerjs';
import { PlannedBackupsComponent } from './planned-backups/planned-backups.component';
import { CronLabelComponent } from './settings-components/cron-label/cron-label.component';
import { SendSettingsComponent} from './settings-components/send-settings/send-settings.component';
import { MatSortModule, MatDialogModule, MatExpansionModule } from '@angular/material';
import { SendNewSettingsComponent } from './send-new-settings/send-new-settings.component';
import { DaemonsInfoComponent } from './daemons-info/daemons-info.component';
import { ErrorInfoComponent } from './daemons-info/error-info/error-info.component';
import { rowIdService } from './daemons-info/service';
import { UpdateSettingsComponent } from './planned-backups/update-settings/update-settings.component';
import { checkAndUpdateView } from '@angular/core/src/view/view';
import { DaemonAdderComponent } from './daemons/daemon-adder/daemon-adder.component';
import { DaemonsRequestComponent } from './daemons/daemons-request/daemons-request.component';
import { PathsComponent } from './planned-backups/paths/paths.component';
import { ChangeEmailComponent } from './admin-settings/change-email/change-email.component';
import { ChangePasswordComponent } from './admin-settings/change-password/change-password.component';
import { AddDatabaseComponent } from './daemons-info/add-database/add-database.component';
import { SendNewSettingsOnetimeComponent } from './send-new-settings-onetime/send-new-settings-onetime.component';


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
    SendSettingsComponent,
    SendNewSettingsComponent,
    DaemonsInfoComponent,
    DaemonAdderComponent,
    DaemonsRequestComponent,
    ErrorInfoComponent,
    UpdateSettingsComponent,
    ChangeEmailComponent,
    ChangePasswordComponent,
    AddDatabaseComponent,
    PathsComponent,
    SendNewSettingsOnetimeComponent
  ],
  entryComponents: [
    ErrorInfoComponent,
    UpdateSettingsComponent,
    SendNewSettingsComponent,
    DaemonAdderComponent,
    DaemonsRequestComponent,
    PathsComponent,
    ChangeEmailComponent,
    ChangePasswordComponent,
    AddDatabaseComponent,
    SendNewSettingsOnetimeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    HttpModule,
    DataTableModule,
    RouterModule.forRoot([
      {path: '', component: LoginComponent},
      {path: 'register', component: RegisterComponent},
      {path: 'home', component: HomeComponent},
      {path: 'admin-settings', component: AdminSettingsComponent},
      {path: 'backup-reports', component: BackupReportsComponent},
      {path: 'send-new-settings', component: SendNewSettingsComponent},
      {path: 'login', component: LoginComponent},
      {path: 'planned-backups', component: PlannedBackupsComponent},
      {path: 'daemons', component: DaemonsComponent},
      {path: 'daemons-info/:id', component: DaemonsInfoComponent}
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
    MatSortModule,
    MatSelectModule,
    MatDialogModule,
    MatExpansionModule,
    MatCardModule,
    MatTooltipModule,
  ],
  providers: [
    rowIdService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
