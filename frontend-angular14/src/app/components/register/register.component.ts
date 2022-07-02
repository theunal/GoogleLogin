import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseModel } from 'src/app/models/response/responseModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup

  constructor(private userService: UserService, private toastrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm()
  }

  createRegisterForm() {
    this.registerForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    })
  }

  register() {
    if (this.registerForm.valid) {
      this.userService.register(this.registerForm.value).subscribe((res: ResponseModel) => {
        console.log(res)
        this.toastrService.success(res.message)
        this.router.navigate(['/login'])
      }, err => {
        console.log(err.error)
        this.toastrService.warning(err.error)
      })
    } else this.toastrService.warning('lütfen alanları doldurunuz')
  }
  
}
