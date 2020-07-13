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
  error: any;
  count: number;

  constructor(private apollo: Apollo) { this.count = 1; }

  ngOnInit() {
    let str = sessionStorage.getItem('token');
    if (str == null) { str = ''; }

    this.apollo
      .query<any>({
        query: gql`{ roleItems { id, name } }`,
        context: {
          headers: {
            token: str
          }
        }
      })
      .subscribe(
        ({ data }) => {
          console.log(sessionStorage.getItem('token'))
          this.roles = data && data.roleItems;
        },
        error => {
          this.error = error;
          console.log(error);
        }
      );
  }
}
