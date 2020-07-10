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
            likes,
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

}
