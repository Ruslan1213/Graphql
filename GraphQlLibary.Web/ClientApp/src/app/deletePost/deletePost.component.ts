import { Component } from "@angular/core";
import { ActivatedRoute, Router } from '@angular/router';
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";

@Component({
  selector: 'app-deletePost',
  styleUrls: ['./deletePost.component.css'],
  template:
    `<h2 class="text-danger">Are you sure to want delete this?</h2>
     <app-postDetails></app-postDetails>
     <button (click)="onClickMe()" class="btn btn-danger">Delete</button>`
})

export class DeletePostComponent {

  post: any;
  stringQuery: string;

  constructor(
    private apollo: Apollo,
    private route: ActivatedRoute,
    private router: Router) {

    this.post = null;
    this.route.queryParams.subscribe(params => {
      this.stringQuery = `mutation
      {
         deletePost(post:{
         id:` + params['id'] + `})
         {
            id
         }
       }`
    });
  }

  onClickMe() {
    this.apollo.mutate<any>({
      mutation: gql(this.stringQuery)
    }).subscribe(
      ({ data }) => {
        this.post = data && data.post;

        if (this.post == null) {
          this.router.navigate(['/404']);
        }
      },
      error => {
        console.log(error);
      }
    );
  }
}
