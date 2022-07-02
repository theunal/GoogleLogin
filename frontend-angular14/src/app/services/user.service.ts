import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response/responseModel';
import { UserRegisterDto } from '../models/userRegisterDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  isAuth: boolean = false

  constructor(private httpClient: HttpClient, private router : Router, private toastrService : ToastrService) { }

  register(user: UserRegisterDto): Observable<ResponseModel> {
    let url = 'https://localhost:7250/api/Users/add'
    return this.httpClient.post<ResponseModel>(url, user)
  }

  login(email: string, password: string) {
    let url = 'https://localhost:7250/api/Users/login?email=' + email + '&password=' + password
    return this.httpClient.post(url, '')
  }

  isAuthenticated() {
    if (localStorage.getItem('token'))
      this.isAuth = true
    else
      this.isAuth = false
      return this.isAuth
  }

  logout() {
    localStorage.removeItem('token')
    this.isAuthenticated()
    this.router.navigate(['/login'])
    this.toastrService.success('başarıyla çıkış yaptınız')
  }

}
