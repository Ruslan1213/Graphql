import { FormGroup, FormControl } from '@angular/forms';
import { Component} from "@angular/core";
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './createPost.component.html'
})

export class CreatePostComponent {

  post: any;
  query: string;
  http: HttpClient;
  fileToUpload: File;
  profileForm = new FormGroup({
    description: new FormControl(''),
  });

  constructor(http: HttpClient, private router: Router) {
    this.http = http;
  }

  onSubmit() {
    let formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);
    formData.append('description', this.profileForm.controls['description'].value);
    const myHeaders = new HttpHeaders().set('token', sessionStorage.getItem('token'));

    this.http.post("https://localhost:44338/api/post/CreatePost", formData, { headers: myHeaders }).subscribe((val) => {
      this.router.navigate(['/post']);
      console.log(val);
    });
    
    return null;
  }

  postMethod(files: FileList) {
    this.fileToUpload = files.item(0);
  }
}
