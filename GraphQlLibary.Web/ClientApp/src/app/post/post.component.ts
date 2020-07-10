import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";

@Component({
  selector: 'app-home',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})

export class PostComponent {
  posts: any[];
  query: string;
  constructor(private apollo: Apollo) {
    this.query = `{postItems{ id, description, likes, dateOfPost, photoUri, user { name } } }`
  }

  ngOnInit() {
    this.apollo
      .query<any>({ query: gql(this.query) })
      .subscribe(({ data }) => {
        this.posts = data && data.postItems;
      },
        error => {
          console.log(error);
        }
      );
  }
}
