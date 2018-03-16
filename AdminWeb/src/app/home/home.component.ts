import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams,HttpClientJsonpModule} from '@angular/common/http';
import {Settings} from './Settings';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  readonly Root_URL = 'http://localhost:54736'; /*http://localhost:54736 */
  /*posts: Observable<any>;*/
  newPost:any;
  PostPok:any;
  constructor(private http: HttpClient) {}
  
  async getPosts(){
    this.http.get<string>(this.Root_URL+"/api/daemon")
    .subscribe(responce => {
      let answ = JSON.parse(responce);
      this.PostPok = answ["RunAt"]
        })
  }
  createPost()
  {
    const data: Settings ={
      DeamonId: 1,
      RunAt: new Date ,
      Cron: "Cron",
      BackupType: "FULL",
      SourcePath: "C:",
      DestinationPath: "C:"
    }
    this.newPost = this.http.post(this.Root_URL+ '/api/admin',JSON.stringify(data));
    
  }

  ngOnInit() {
  }
  

}
