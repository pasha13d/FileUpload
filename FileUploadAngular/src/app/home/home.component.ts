import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import {HttpEventType, HttpErrorResponse} from '@angular/common/http';
import {of} from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import {UploadService} from '../upload.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // Variable to store shortLink from api response
  shortLink: string = "";
  loading: boolean = false; // Flag variable
  file: File = null; // Variable to store file
  message: string;

  // Inject service 
  constructor(private fileUploadService: UploadService) { }

  ngOnInit(): void {
  }

  // On file Select
  onChange(event) {
      this.file = event.target.files[0];
  }

  // OnClick of button Upload
  onUpload() {
      this.loading = !this.loading;
      console.log(this.file);
      this.fileUploadService.upload(this.file).subscribe(
          (response: any) => {
            console.log(response);
            if(response && response.Value)
            {
              this.message = response.Value;
            }
            this.loading = false;
            // if (typeof (response) === 'object') {
            //     // Short link via api response
            //     this.shortLink = response.link;
            //     this.loading = false; // Flag variable 
            // }
          }
      );
  }
  

}
