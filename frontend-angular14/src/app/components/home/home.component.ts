import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  jwtHelper: JwtHelperService = new JwtHelperService()

  userName : string = ''

  constructor(private userService : UserService) { }

  ngOnInit(): void {
    this.refresh()
  }

  refresh() {
    if (this.userService.isAuthenticated()) {
      let decode = this.jwtHelper.decodeToken(localStorage.getItem('token'))
      this.userName = decode[Object.keys(decode).filter(x => x.endsWith('/name'))[0]]
    }
  }

  logout() {
    this.userService.logout()
  }
}
