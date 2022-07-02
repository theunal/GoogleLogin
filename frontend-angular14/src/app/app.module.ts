import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HttpClientModule } from '@angular/common/http'
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SocialLoginModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }), // konsolda ngModel için sarı uyarı vermesin diye
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      progressBar: true,
      progressAnimation: 'increasing',
      timeOut: 3000
    })
  ],
  providers: [
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,


            // google cloud daki client id
            provider: new GoogleLoginProvider('194099528747-hadb8mf9uhmk5q28fmb22oihrnjhaifk.apps.googleusercontent.com')
          }
        ],
        onError: err => console.log(err)
      } as SocialAuthServiceConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
