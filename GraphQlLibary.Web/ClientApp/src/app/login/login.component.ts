import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";

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

  constructor(private apollo: Apollo) { }

  onSubmit() {
 
    console.log(this.profileForm.controls['email'].value);
    console.log(this.profileForm.controls['password'].value);

    this.apollo.mutate({
      mutation: null,
      variables: {
        file: this.profileForm.value,
      }
    }).subscribe(
      ({ data }) => {
      },
      error => {
        console.log(error);
      }
    );
  }
}
