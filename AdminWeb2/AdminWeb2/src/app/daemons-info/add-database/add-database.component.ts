import { Component, OnInit, Inject, Output } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { Http, Headers, RequestOptions, HttpModule} from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Databases } from './Databases';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { MatSnackBar, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { DataTableResource } from 'angular5-data-table';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
@Component({
  selector: 'app-add-database',
  templateUrl: './add-database.component.html',
  styleUrls: ['./add-database.component.css']
})
export class AddDatabaseComponent implements OnInit {

  constructor(private http: HttpClient, private route: ActivatedRoute) {
    this.sub = this.route.params.subscribe(params => {
    });
   }
   sub: any;
   DaemonId: number
   headers = new HttpHeaders();
   displayedColumns = ['SrvName','DbName', 'Login','Password'];
   tableSource: MatTableDataSource<Databases>;
  ngOnInit() {
    this.DaemonId = +localStorage.getItem("DaemonId")
    this.http.get<Databases[]>('http://localhost:63324/api/admin/daemons/databases/'+this.DaemonId, {headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
    .subscribe  (data => {
      this.tableSource = new MatTableDataSource(data);
    })
  }
  AddDatabase(SrvName:string,DbName:string,Login:string,Password:string){
    const head =  {headers: new  HttpHeaders({'Content-Type':'application/json'}) };
    head.headers.append('Content-Type', 'application/json')
    const data: Databases={
      ServerName:SrvName,
      Name:DbName,
      Login:Login,
      Password:Password,
      DaemonId: this.DaemonId
    }
    this.http.post<boolean>('http://localhost:63324/api/admin/daemons/AddDatabase',JSON.stringify(data),{headers: new HttpHeaders( {"Authorization": "Bearer " + localStorage.getItem("Token"), 'Content-Type':'application/json'})})
    .subscribe(Response=>{
    console.log(Response)
    })
  }

}
