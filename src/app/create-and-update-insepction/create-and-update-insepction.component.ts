import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Inspection } from '../inspection';
import { InspectionSvcService } from '../inspection-svc.service';
import { Insepector } from '../insepector.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { APP_DATE_FORMATS, AppDateAdapter } from '../date.adapter';



@Component({
  selector: 'app-create-and-update-insepction',
  templateUrl: './create-and-update-insepction.component.html',
  styleUrls: ['./create-and-update-insepction.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: DateAdapter, useClass: AppDateAdapter
    },
    {
      provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
    }
  ]
})
export class CreateAndUpdateInsepctionComponent implements OnInit {

  InspectionFG: FormGroup;
  submitted = false;
  ObjInspectionModel: Inspection = new Inspection();
  Lstinsepector: Insepector[] = [];
  btnName : string;
  message : string = "";

  constructor(private formBuilder: FormBuilder,
    private InspectionSvc: InspectionSvcService,
    private _mdr: MatDialogRef<CreateAndUpdateInsepctionComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) {
    this.ObjInspectionModel = data.InspectionModel;

    if(this.ObjInspectionModel.InspectionID == undefined ){
      this.btnName = 'Create';
    } else {
      this.btnName = 'Update';
    }

    this.GetAllInsepctor();
  }

  ngOnInit(): void {
    this.InspectionFG = this.formBuilder.group(
      {
        InspectionDt: ["", [Validators.required]],
        CustomerName: ["", [Validators.required]],
        Address: ["", [Validators.required]],
        Observations: ["", [Validators.required]],
        Status: ["", [Validators.required]],
        Inspector: ["", [Validators.required]]
      }
    )

    if(this.btnName === 'Update'){
      this.InspectionFG.controls['InspectionDt'].setValue(this.ObjInspectionModel.InspectionDate);
      
    }
  }



  get f1() { return this.InspectionFG.controls; }
  GetAllInsepctor() {
    this.InspectionSvc.getallregisterinspector().subscribe(x => {
      if (x.IsSuccess) {
        this.Lstinsepector = x.Inspector
      }
    })
  }
  OnSubmit() {
    if (this.InspectionFG.invalid) {
      return;
    }

    this.InspectionSvc.createandupdateinspection(this.ObjInspectionModel).subscribe(x=> {
      if(!x.IsSuccess){
        alert(x.Message)
         // this.message = x.Message;
      } else {
        this._mdr.close({ data: x.Message })
      }
    })

    
  }

  CloseDialog() {
    this._mdr.close(true)
  }
}
