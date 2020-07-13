import { FormGroup, FormControl } from '@angular/forms';
import { Component } from "@angular/core";
import { Apollo } from "apollo-angular";
import { HttpHeaders } from "@angular/common/http";
import gql from "graphql-tag";
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})

export class LoginComponent {

  post: any;
  query: string;

  profileForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private apollo: Apollo, private router: Router) { }


  onSubmit() {
 
    let mail = this.profileForm.controls['email'].value;
    let password = this.profileForm.controls['password'].value;

    let str = `mutation { sessions(mail:"` + mail + `", password:"` + password+`") }`
    this.apollo.mutate<any>({
      mutation: gql(str)
    }).subscribe(
      ({ data }) => {

        let headers: HttpHeaders = new HttpHeaders();
        headers = headers.append('token', data.sessions);
        sessionStorage.setItem('token', data.sessions);

        if (sessionStorage != null) {
          this.router.navigate(['/']);
        } else { alert('password or login is invalid');}
      },
      error => {
        console.log(error);
      }
    );
  }
}
