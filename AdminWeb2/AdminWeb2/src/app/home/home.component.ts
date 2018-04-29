import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  Username: string = localStorage.getItem("Username")

  
  Datum: string = new Date().toLocaleDateString()

  ngOnInit() {
  }

  inputs = [1]

  Add()
  {
    this.inputs.push(1)
  }

}