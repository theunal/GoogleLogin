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

  loginForm: FormGroup

  constructor(private userService: UserService, private toastrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.createLoginForm()
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.userService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe((res : any) => {
        localStorage.setItem('token', res.token)
        this.router.navigate([''])
        this.toastrService.success('Giriş Başarılı.')
      }, err => {
        this.toastrService.error(err.error)
      })
    } else this.toastrService.warning('lütfen bilgileri doldurunuz.')

  }
}
