import { Component, OnInit } from "@angular/core";
import { Apollo } from "apollo-angular";
import gql from "graphql-tag";

@Component({
  selector: "app-role",
  templateUrl: "./role.component.html",
  styleUrls: ["./role.component.css"]
})
export class RoleComponent implements OnInit {
  roles: any[];
  loading = true;
  error: any;
  count: number;
  constructor(private apollo: Apollo) { this.count = 1; }

  ngOnInit() {
    this.apollo
      .query<any>({
        query: gql`
          {
            roleItems {
              id,
              name
            }
          }
        `
      })
      .subscribe(
        ({ data, loading }) => {
          this.roles = data && data.roleItems;
          this.loading = loading;
        },
        error => {
          this.loading = false;
          this.error = error;
        }
      );
  }

  getAuthorNames(authors) {
    if (authors.length > 1)
      return authors.reduce((acc, cur) => acc.name + ", " + cur.name);
    else return authors[0].name;
  }

  onClick()
  {
    this.count = this.count + 1;
  }
}
