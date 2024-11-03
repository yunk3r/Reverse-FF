import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { QuestionAnswer } from "../models/questionAnswer";
import { default_settings, Settings } from "../models/settings";


@Injectable({
    providedIn: 'root',
   })
export class QuestionAnswerState {

    public currentQuestion$ = new BehaviorSubject<QuestionAnswer | null>(null);
    public lastScore$ = new BehaviorSubject<Number | null>(null);
    public show$ = new BehaviorSubject<boolean>(false);
    public settings$ = new BehaviorSubject<Settings>(default_settings);
    public showMatching$ = new BehaviorSubject<boolean>(false);
    public submitting$ = new BehaviorSubject<boolean>(false);

    constructor() {
        const settings = localStorage.getItem('Settings');
        if(!!settings) {
            this.settings$.next(JSON.parse(settings));
        }
        this.settings$.subscribe(s => {
            localStorage.setItem('Settings', JSON.stringify(s));
        });

        this.show$.subscribe(x =>{
            if(x === true) {
                this.showMatching$.next(false);
            }
        })
    }

}