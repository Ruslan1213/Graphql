import { FormGroup, FormControl } from '@angular/forms';
import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";

const FileUpload = gql`mutation ($file: Upload!) { singleUpload(file: $file)}`;
//"{ 
@Component({
  selector: 'app-home',
  templateUrl: './createPost.component.html'
})

export class CreatePostComponent {

  post: any;
  query: string;

  profileForm = new FormGroup({
    file: new FormControl(''),
    //description: new FormControl(null, [Validators.required]),
  });

  constructor(private apollo: Apollo) { }

  onSubmit() {
 
    console.log(this.profileForm.controls['file'].value);

    this.apollo.mutate({
      mutation: FileUpload,
      variables: {
        file: this.profileForm.controls['file'].value,
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
