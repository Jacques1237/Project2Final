import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { IImage } from '../Interfaces/IImage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public progress: number;
  public message: string;
  public isCreate: boolean;
  public Description: string;
  public featured: boolean;
  public Upimage: IImage = {
    imagesId: null,
    imagesDescription: '',
    imagesPath: '',
    featured: false,
  }
  public fileToUpload: File | null = null;
  public images: IImage[] = [];
  public response: { dbPath: '' };
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.isCreate = true;
    this.loadGallery();
  }

  private loadGallery(): void {
  
    this.http.get('/api/Gallery')
      .subscribe((res:any )=> {
        this.images = res as IImage[]
        console.log(this.images);
      })
}

  public deleteGalleryPhoto(ID:number): void{

    this.http.delete('/api/Gallery?id='+ID)
      .subscribe((res: any) => {
        if (res) {
          this.loadGallery();
        }
        else
          console.error("image delete error", ID)
         console.log(res);
      })
  }
  public setFile(files) {
    if (files.length === 0) {
      return files;
    }
    this.fileToUpload = <File>files[0];
    //this.fileToUpload
  }

  public uploadFile  ()  {
    
    if (this.fileToUpload == null) {
      return;
    }
  
    const formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);
    formData.append('ImagesDescription', this.Upimage.imagesDescription);
    formData.append('Featured', this.Upimage.featured.toString());
    this.http.post('/api/Gallery', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.fileToUpload = null;
          this.onUploadFinished.emit(event.body);
          this.loadGallery();
        }
      });
  }

 // private getUsers = () => {
   // this.http.get('/api/Gallery')
    //  .subscribe(res => {
      //  this.images = res as IImage[];
     // });

 // }

  public uploadFinished = (event) => {
    this.response = event;
   }

     public createImgPath = (serverPath: string) => { //?
       return `/${serverPath}`;
    }
}
