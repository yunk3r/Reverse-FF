import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { QuestionAnswer } from "../models/questionAnswer";


@Injectable({
    providedIn: 'root',
   })
export class QuestionAnswerService {
  
    public baseUrl!: URL;
    constructor(private http: HttpClient) { 
      this.baseUrl = new URL(document.getElementsByTagName('base')[0].href);
      if(environment.production) {
      this.baseUrl.port = window.location.port;
      } else {
        this.baseUrl.port = '5401'
      }
    }
  
    public getRandomQuestion(): Observable<QuestionAnswer> {
      const url = `${this.baseUrl}QuestionAnswer/GetRandomQuestionAnswer`
      return this.http.get<QuestionAnswer>(url);
    }
  }