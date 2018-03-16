import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router';
import { HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AdminComponent } from './admin/admin.component';
import { DeamonsComponent } from './deamons/deamons.component';
import { BsNavbarComponent } from './bs-navbar/bs-navbar.component';
import { DeamonsSettingsComponent } from './deamons-settings/deamons-settings.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    AdminComponent,
    DeamonsComponent,
    BsNavbarComponent,
    DeamonsSettingsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    
    RouterModule.forRoot([ 
{path: '', component: LoginComponent},
{path: 'Register', component: RegisterComponent},
{path: 'Admin', component: AdminComponent},
{path: 'Deamons', component: DeamonsComponent},
{path: 'Home', component: HomeComponent},
{path: 'Deamon/Settings', component: DeamonsSettingsComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
