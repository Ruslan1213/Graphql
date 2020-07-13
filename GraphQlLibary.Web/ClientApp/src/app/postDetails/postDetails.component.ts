import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-postDetails',
  templateUrl: './postDetails.component.html',
  styleUrls: ['./postDetails.component.css']
})
export class PostDetails {

  post: any;
  loading = true;
  error: any;
  id: string;
  stringQuery: string;

  constructor(private apollo: Apollo, private route: ActivatedRoute, private router: Router,) {
    this.post = null;
    this.id = '-1';
    this.route.queryParams.subscribe(params => {
      this.stringQuery = `
        {
          post(id: "`+ params['id'] + `"){
            id,
            description,
            likes { postId },
            dateOfPost,
            photoUri
            user{
              name
            }
          }
        }
        `
    });
  }

  ngOnInit() {      

    this.apollo
      .query<any>({
        query: gql(this.stringQuery)
      })
      .subscribe(
        ({ data, loading }) => {
          this.post = data && data.post;
          this.loading = loading;

          if (this.post==null) {
             this.router.navigate(['/404']);
          }

          console.log(this.post);
        },
        error => {
          this.loading = false;
          this.error = error;
        }
      );
  }

  updateLikes(id) {

    let str = `mutation {
      likePost(postId: `+ id +`, userId: 1)
    }`;

    this.apollo
      .mutate<any>({
        mutation: gql(str)
      })
      .subscribe(
        ({ data }) => {
          console.log(data.likePost);

          if (data.likePost.length == false) {
            alert('your already like this post');
          } else {
            this.post.likes.length += 1;
          }
        },
        error => {
          this.error = error;
        }
    );

   
  }
}
