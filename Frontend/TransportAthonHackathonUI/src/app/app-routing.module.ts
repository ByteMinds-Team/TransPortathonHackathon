import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeLayoutComponent } from './layouts/home-layout/home-layout.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AppoinmentComponent } from './components/appointment/appoinment.component';
import { CorporateVehicleComponent } from './components/corporate-vehicle/corporate-vehicle.component';
import { MessageComponent } from './components/message/message.component';
import { CorporateCrewComponent } from './components/corporate-crew/corporate-crew.component';
import { AppointmentDetailComponent } from './components/appointment-detail/appointment-detail.component';
import { ProfileComponent } from './components/profile/profile.component';
import { VehicleDetailComponent } from './components/vehicle-detail/vehicle-detail.component';
import { MyOfferComponent } from './components/my-offer/my-offer.component';
import { CompanyProfileComponent } from './components/company-profile/company-profile.component';
import { CorporateRegisterComponent } from './components/corporate-register/corporate-register.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeLayoutComponent,
    children: [
      {
        path: 'companyInfo/:id',
        component: CompanyProfileComponent
      },
      {
        path: 'offers/:id',
        component: HomeComponent,
      },
      {
        path: 'offers',
        redirectTo: 'offers/0'
      },
      {
        path: 'myoffers',
        component : MyOfferComponent
      },
      {
        path: 'appointments',
        component: AppoinmentComponent
      },
      {
        path: 'appointments/:id',
        component: AppointmentDetailComponent
      },
      {
        path: 'messages',
        component: MessageComponent
      },
      {
        path: 'vehicles',
        component: CorporateVehicleComponent
      },
      {
        path: 'crews',
        component: CorporateCrewComponent
      },
      {
        path: 'profile',
        component: ProfileComponent
      },
      {
        path: 'vehicles/:id',
        component: VehicleDetailComponent
      }
    ],
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'corporate-register',
    component: CorporateRegisterComponent
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home/offers'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
