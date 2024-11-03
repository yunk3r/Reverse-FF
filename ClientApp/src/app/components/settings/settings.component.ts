import { Component, OnInit, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { take } from 'rxjs';
import { QuestionAnswerState } from 'src/app/State/QuestionAnswerState';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  public difficulty = new FormControl('Mid');

  constructor(public dialogRef: MatDialogRef<SettingsComponent>,
    public state: QuestionAnswerState) { }

  ngOnInit(): void {
    this.state.settings$.pipe(take(1)).subscribe(setting => {
      this.difficulty.setValue(setting.difficulty);
    });
    this.difficulty.valueChanges.subscribe(d => {
      const s = this.state.settings$.getValue();
      s.difficulty = d ?? 'Mid';
      this.state.settings$.next(s);
    })
  }

  public close() {
    this.dialogRef.close();
  }

}
