import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/response/responseModel';
import { UserRegisterDto } from '../models/userRegisterDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  register(user: UserRegisterDto): Observable<ResponseModel> {
    let url = 'https://localhost:7250/api/Users/add'
    return this.httpClient.post<ResponseModel>(url, user)
  }
}
