import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { ApolloModule, APOLLO_OPTIONS } from "apollo-angular";
import { HttpLinkModule, HttpLink } from "apollo-angular-link-http";
import { InMemoryCache } from "apollo-cache-inmemory";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PostComponent } from './post/post.component';
import { RoleComponent } from './role/role.component';
import { FooterComponent } from './footer/footer.component';
import { PostDetails } from './postDetails/postDetails.component';
import { DeletePostComponent } from './deletePost/deletePost.component';
import { CreatePostComponent } from './createPost/createPost.component';
import { NotFoundComponent } from './notFound/notFound.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PostComponent,
    RoleComponent,
    FooterComponent,
    PostDetails,
    NotFoundComponent,
    DeletePostComponent,
    CreatePostComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ApolloModule,
    FormsModule,
    HttpLinkModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'post', component: PostComponent },
      { path: 'role', component: RoleComponent },
      { path: 'post/details', component: PostDetails },
      { path: '404', component: NotFoundComponent },
      { path: 'post/delete', component: DeletePostComponent },
      { path: 'post/create', component: CreatePostComponent },
      { path: 'login', component: LoginComponent },
    ])
  ],
  providers: [{
    provide: APOLLO_OPTIONS,
    useFactory: (httpLink: HttpLink) => {
      return {
        cache: new InMemoryCache(),
        link: httpLink.create({
          uri: "https://localhost:44338/graphql"
        })
      }
    },
    deps: [HttpLink]
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
