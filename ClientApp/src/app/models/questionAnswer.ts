export interface QuestionAnswer {
    questionNumber: Number;
    question: string;
    answers: Answer[];
}

export interface Answer {
    answerText: string;
    points: number;
}

