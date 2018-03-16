import { Component } from '@angular/core';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { RouterLink, Route } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { Router} from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  readonly Root_URL = 'https://jsonplaceholder.typicode.com'; /*http://localhost:54736 */
  posts: any;
  constructor(private http: HttpClient,public router: Router) {}
  
  getPosts(){
    this.posts = this.http.get(this.Root_URL+ '/posts')
  }
  

}
