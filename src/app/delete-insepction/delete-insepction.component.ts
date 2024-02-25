import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-insepction',
  templateUrl: './delete-insepction.component.html',
  styleUrls: ['./delete-insepction.component.css']
})
export class DeleteInsepctionComponent implements OnInit {

  InspectionId : number;
  constructor(private _mdr: MatDialogRef<DeleteInsepctionComponent>,
    @Inject(MAT_DIALOG_DATA) data: any){
      this.InspectionId = data.InspectionId
  }
  ngOnInit(): void {
    
  }

  deleteInspection(InspectionId : number){
    this._mdr.close({ data: 'yes',InspectionId :  InspectionId});
  }

  CloseDialog() {
    this._mdr.close(true)
  }
}
