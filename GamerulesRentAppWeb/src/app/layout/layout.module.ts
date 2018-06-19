import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Route } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { HomeComponent } from './home/home.component';

const routes: Route[] = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ],
  declarations: [
    HomeComponent
  ],
  exports: [
  ]
})
export class LayoutModule { }
