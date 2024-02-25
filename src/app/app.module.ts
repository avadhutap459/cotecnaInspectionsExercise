import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InspectionSvcService } from './inspection-svc.service';
import { CreateAndUpdateInsepctionComponent } from './create-and-update-insepction/create-and-update-insepction.component';
import { DatePipe } from '@angular/common';
import { DeleteInsepctionComponent } from './delete-insepction/delete-insepction.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateAndUpdateInsepctionComponent,
    DeleteInsepctionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    InspectionSvcService,
    DatePipe
  ],
  bootstrap: [AppComponent]
  
})
export class AppModule { }
