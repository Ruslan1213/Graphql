import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html"
})
export class RegistrationComponent {
  user: any;
  error: any;
  queryString: string;
  registerForm = new FormGroup({
    email: new FormControl(''),
    name: new FormControl(''),
    password: new FormControl(''),
    passwordRepied: new FormControl(''),
  });

  constructor(private apollo: Apollo) {  }

  onSubmit() {


    let email = this.registerForm.controls['email'].value;
    let name = this.registerForm.controls['name'].value;
    let password = this.registerForm.controls['password'].value;
    let passwordRepied = this.registerForm.controls['passwordRepied'].value;

    if (password != passwordRepied) { alert('password does not match'); return; }

    this.queryString = `mutation{registration(user: {name:"`+name+`", password:"` + password + `", email:"` + email+`" }) { email }}`;

    this.apollo
      .mutate<any>({ mutation: gql(this.queryString) })
      .subscribe(
        ({ data }) => {
          this.user = data && data.user;
        },
        error => {
          this.error = error;
          console.log(error);
        }
      );
  }
}
