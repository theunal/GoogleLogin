import { GoogleLoginDto } from './../../models/googleLoginDto';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  googleLoginUser: GoogleLoginDto
  loginForm: FormGroup

  constructor(private userService: UserService, private toastrService: ToastrService, private router: Router,
    private socialAuthService: SocialAuthService) { }

  ngOnInit(): void {
    this.createLoginForm()
    console.log("isAuth: "+ !this.userService.isAuthenticated())
    if (this.userService.isAuthenticated() === false) {
      console.log("google login çalıştı")
      this.socialAuthService.authState.subscribe((user: SocialUser) => {
        this.googleLoginUser = user
          this.googleLogin()
      })
    } 
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.userService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe((res: any) => {
        localStorage.setItem('token', res.token)
        this.router.navigate([''])
        this.toastrService.success('Giriş Başarılı.')
      }, err => {
        this.toastrService.error(err.error)
      })
    } else this.toastrService.warning('lütfen bilgileri doldurunuz.')
  }




  googleLogin() {
    this.userService.googleLogin(this.googleLoginUser).subscribe((res: any) => {
      localStorage.setItem('token', res.token)
      this.router.navigate([''])
      this.toastrService.success('Giriş Başarılı.')
    }, err => {
      this.toastrService.error(err)
    })
  }


}
