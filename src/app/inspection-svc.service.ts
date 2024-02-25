import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Inspection } from './inspection';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class InspectionSvcService {

  public rooturl = environment.apiEndpoint
  constructor(private _http: HttpClient,private datePipe:DatePipe) {

  }

  getallregisterinspector(): Observable<any> {
    return this._http.get<any>(this.rooturl + '/api/v1/GetAllInspector');
  }
  getallregisterinspecation(): Observable<any> {
    return this._http.get<any>(this.rooturl + '/api/v1/GetAllInspection');
  }
  getallinspecationbyInspectorname(Inspectorname : string) :Observable<any>{
    return this._http.get<any>(this.rooturl + '/api/v1/GetAllInspectionByInspector/' +Inspectorname);
  }

  createandupdateinspection(objInspection: Inspection): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    var body = {
      InspectionID: objInspection.InspectionID,
      InspectionDate: objInspection.InspectionDate ,
      Customer: objInspection.Customer,
      Address: objInspection.Address,
      Observations: objInspection.Observations,
      Status: objInspection.Status,
      InspectorId: objInspection.InspectorId,
      InspectorName: ""
    };
    let options = {
      headers: headers
    }
    return this._http.post<any>(this.rooturl + '/api/v1/PostAndUpdateInspectionData', body, options);
  }

  deleteinspecationbyid(inspectionid : number): Observable<any> {
    return this._http.delete<any>(this.rooturl + '/api/v1/DeleteInspection/'+inspectionid);
  }
}
