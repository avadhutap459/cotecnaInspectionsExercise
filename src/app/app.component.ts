import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { InspectionSvcService } from './inspection-svc.service';
import { Insepector } from './insepector.model';
import { CreateAndUpdateInsepctionComponent } from './create-and-update-insepction/create-and-update-insepction.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Inspection } from './inspection';
import { forkJoin } from 'rxjs';
import { DeleteInsepctionComponent } from './delete-insepction/delete-insepction.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, AfterViewInit {

  title: string = "Angular 16"
  displayedColumns: string[] = ['Inspector', 'InspectionDate', 'Customer', 'Address', 'Observation', 'Status', 'Action'];
  Lstinsepector: Insepector[] = [];
  InspectorName: string = '';
  matDialogRef: MatDialogRef<CreateAndUpdateInsepctionComponent>;
  matDeleteDialogRef: MatDialogRef<DeleteInsepctionComponent>;
  ObjInspectionModel: Inspection = new Inspection();
  message: string = "";
  LstInspection: Inspection[] = [];

  dataSource: MatTableDataSource<Inspection>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;


  constructor(private InspectionSvc: InspectionSvcService,
    private matDialog: MatDialog) {

  }
  ngOnInit(): void {

    // forkJoin([this.GetAllInsepctor(),this.GetAllInspection()]).subscribe();
    this.GetAllInsepctor();
    this.GetAllInspection();

  }
  ngAfterViewInit() {
    //this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    debugger
    const filterValue = (event.target as HTMLInputElement).value;
  
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  filterTable() {
    this.dataSource.filterPredicate = (data: Inspection, filter: string): boolean => {
      return (
        data.Customer.toLocaleLowerCase().includes(filter) || data.Status.toLocaleLowerCase().includes(filter)
      )
    }
  } 
  OnchangeInsepctor(name: any) {
    debugger

    if(name === "" || name === undefined){
      this.GetAllInspection();
    } else {
      this.InspectionSvc.getallinspecationbyInspectorname(name).subscribe(x=> {
        if (x.IsSuccess) {

          this.LstInspection = x.InspectionData;
          this.dataSource = new MatTableDataSource(x.InspectionData);
  
          this.dataSource.paginator = this.paginator;
          this.filterTable();
        } else {
          this.LstInspection = x.InspectionData;
  
          this.dataSource = new MatTableDataSource(x.InspectionData);
  
          this.dataSource.paginator = this.paginator;
          this.filterTable();
        }
      })
    }

  }


  GetAllInsepctor() {
    this.InspectionSvc.getallregisterinspector().subscribe(x => {
      if (x.IsSuccess) {
        this.Lstinsepector = x.Inspector
      }
    })
  }

  GetAllInspection() {
    this.InspectionSvc.getallregisterinspecation().subscribe(x => {
      debugger
      if (x.IsSuccess) {

        this.LstInspection = x.InspectionData;
        this.dataSource = new MatTableDataSource(x.InspectionData);

        this.dataSource.paginator = this.paginator;
        this.filterTable();
      } else {
        this.LstInspection = x.InspectionData;

        this.dataSource = new MatTableDataSource(x.InspectionData);

        this.dataSource.paginator = this.paginator;
        this.filterTable();
      }
    })
  }

  CreateInspection() {

    debugger
    this.ObjInspectionModel = new Inspection();
    
    this.matDialogRef = this.matDialog.open(CreateAndUpdateInsepctionComponent, {
      data: { InspectionModel: this.ObjInspectionModel },
      disableClose: true
    });

    this.matDialogRef.afterClosed().subscribe(res => {
      debugger;
      if ((res == true)) {
        
        //this.name = "";
      } else if(res.data === "Data insert successfully"){
        this.message = res.data
        this.GetAllInspection();
      }
    });

  }
  EditInspection(InspectionId: any) {
    debugger;
    var InspectionDataModel = this.LstInspection.filter(x=>x.InspectionID === InspectionId)[0]
    this.matDialogRef = this.matDialog.open(CreateAndUpdateInsepctionComponent, {
      data: { InspectionModel: InspectionDataModel },
      disableClose: true
    });

    this.matDialogRef.afterClosed().subscribe(res => {
      debugger;
      if ((res == true)) {
        //this.name = "";
      }else if(res.data === "Data update successfully"){
        this.message = res.data
        this.GetAllInspection();
      }
    });
  }

  DeleteInspection(InspectionId: any) {
    debugger
    this.matDeleteDialogRef = this.matDialog.open(DeleteInsepctionComponent, {
      data: { InspectionId: InspectionId },
      disableClose: true
    });

    this.matDeleteDialogRef.afterClosed().subscribe(res => {
      debugger;
      if ((res.data == "yes")) {
        this.InspectionSvc.deleteinspecationbyid(res.InspectionId).subscribe(x=> {
          if(x.IsSuccess){
            this.message = x.Message
            this.GetAllInspection();

          }
        })
        
        
        //this.name = "";
      }
    });
  }
}
