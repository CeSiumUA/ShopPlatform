import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: "any"
})
export class FiletransferService{
  constructor(private http: HttpClient) {
  }
  public uploadFiles(files: any, fileRef: FileRef): Observable<any>{
    const formData = new FormData();
    for (let file of files){
      formData.append(file.name, file);
    }
    let url = `/cdn/files/${fileRef}/uploadicon`;
    return this.http.post(url, formData);
  }
}
export enum FileRef{
  Profile = 'profiles',
  Item = 'items',
  Shop = 'shops'
}
