import { Component, OnInit } from '@angular/core';
import * as use from '../utils/universal-sentence-encoder';
import * as tf from '@tensorflow/tfjs';
import { QuestionAnswerState } from '../State/QuestionAnswerState';
import { QuestionAnswerService } from '../Services/question-answer.service';
import { FormControl } from '@angular/forms';
import { take } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { SettingsComponent } from '../components/settings/settings.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {


public model!: use.UniversalSentenceEncoder;

public questionInput = new FormControl('');

public matchingWords = '';

  constructor(public state: QuestionAnswerState,
    private questionAnswerService: QuestionAnswerService,
    public dialog: MatDialog) { }

  async ngOnInit(): Promise<void> {
    this.getRandomQuestion();
    this.model = await use.load();
  }

  public async submit() {
    try {
      this.state.submitting$.next(true);
      const question =  this.state.currentQuestion$.getValue()?.question ?? '';
      let input = this.questionInput.value ?? '';
      if(input.endsWith('.')) {
        input = input.substring(0, input.length - 1);
      }
      const sentences = [question, input]
      const embeddings = await this.model.embed(sentences);

      const sentenceI = tf.slice(embeddings, [0, 0], [1]);
      const sentenceJ = tf.slice(embeddings, [1, 0], [1]);
      const sentenceITranspose = false;
      const sentenceJTransepose = true;

      const score = (await tf.matMul(sentenceI, sentenceJ,sentenceITranspose, sentenceJTransepose)
      .data())[0] * 100;

      this.state.lastScore$.next(Number((score).toFixed(2)));
      this.state.submitting$.next(false);
      let target = 70;
      if(this.state.settings$.getValue().difficulty === 'Easy') {
        target = 65;
      } else if (this.state.settings$.getValue().difficulty === 'Mid') {
        target = 70;
      } else if (this.state.settings$.getValue().difficulty === 'Hard') {
        target = 80;
      }
      if(score > target) {
        this.state.show$.next(true);
      }
    } catch {
      this.state.submitting$.next(false);
    }
      
  }

  public getRandomQuestion() {
    this.questionAnswerService.getRandomQuestion().pipe(take(1)).subscribe(x => {
      this.state.currentQuestion$.next(x);
    });
    this.state.lastScore$.next(null);
    this.state.show$.next(false);
    this.questionInput.setValue('');
  }

  public reveal() {
    this.state.show$.next(true);
  }

  public matching() {
    this.state.showMatching$.next(true);
    const input = this.questionInput.value?.split(' ');
    const questions = this.state.currentQuestion$.getValue()?.question?.split(' ');
    this.matchingWords = '';
    input?.forEach(word => {
      if(questions?.find(q => q?.toUpperCase()?.replace(/[^\w\s]/gi, "") === word?.toUpperCase() )) {
        this.matchingWords = this.matchingWords + word + ' ';
      }
    })

  }

  public openSettings(){
    this.dialog.open(SettingsComponent, {
      width: '100%',
      height: '100%',
      maxWidth: '400px',
      maxHeight: '300px'
    });
  }

}
