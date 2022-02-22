import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyWoundDto } from 'src/app/DTOs/MyWoundDto';
import { FileUploadDto } from 'src/app/DTOs/FileUploadDto';
import { WoundPhotoDetailDto } from 'src/app/DTOs/WoundPhotoDetailDto';
import { DomSanitizer } from "@angular/platform-browser";
import { Location } from '@angular/common';

@Component({
  selector: 'app-wound-details',
  templateUrl: './wound-details.component.html',
  styleUrls: ['./wound-details.component.css']
})
export class WoundDetailsComponent implements OnInit {
  woundId!: string | null;

  userId!: number | undefined;
  userEmail!: string;
  isPatient!: boolean | undefined;
  isDoctor!: boolean | undefined;

  woundDetails!: MyWoundDto;
  photoDetails!: WoundPhotoDetailDto[];

  selectedFile!: File;
  isFileSelected = false;

  image!: any;

  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router, private sanitizer: DomSanitizer, private _location: Location) { 
    this.userId = this.api.loggedUserId;
    this.userEmail = this.api.loggedUserEmail;
    this.isPatient = this.api.loggedUserIsPatient;
    this.isDoctor = this.api.loggedUserIsDoctor;
  }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }


  async loadData() {
    console.log('load data');
    await this.loadWoundDetails();
    await this.loadWoundPhotoDetails();
  }

  async loadWoundDetails() {
    console.log('load wound details for id: ', this.woundId);
    this.woundDetails = await this.api.getWoundDetails(this.woundId);
    console.log('loaded wound details: ', this.woundDetails);
  }

  async loadWoundPhotoDetails() {
    console.log('load wound photo details for wound id: ', this.woundId);
    this.photoDetails = await this.api.getWoundPhotoDetails(this.woundId);
    console.log('loaded wound photo details: ', this.photoDetails);
  }

  async onFileChanged(event: any){
    console.log('on file changed event: ', event);
    this.selectedFile = event.target.files[0];
    this.isFileSelected = true;
    console.log('selected file: ', this.selectedFile);
  }

  async onUpload() {
    console.log('on upload');
    let fileUploadDto: FileUploadDto = {
      file: this.selectedFile,
      filename: this.selectedFile.name,
      woundId: this.woundId
    }
    console.log('file dto: ', fileUploadDto);
    await this.api.uploadPhoto(fileUploadDto);
    this.isFileSelected = false;
    console.log('file uploaded');
    await this.loadWoundPhotoDetails();
  }

  async onImageClick(photo: any){
    console.log('selected image: ', photo);
    let photoId = photo.woundPhotoId;
    const filename = await this.api.getPhoto2(photoId);
    console.log('filename: ', filename);
    let imgPath = `/assets/images/${photoId}.jpeg`;
    window.open(imgPath);
  }

  ngOnInit(): void {
      this.route.paramMap.subscribe( params => {
        let id = params.get('woundId');  
        console.log('got wound id: ', id);
        this.woundId = id;
      });
      (async () => {
        await this.loadData();
      })();
  }
}