﻿using Nabeey.Service.DTOs.Answers;
using Nabeey.Service.DTOs.Questions;
using Nabeey.Service.DTOs.Quizzes;
using Nabeey.Service.DTOs.Users;

namespace Nabeey.Service.DTOs.QuestionAnswers;

public class QuestionAnswerResultDto
{
    public bool IsTrue { get; set; }
    public AnswerResultDto Answer { get; set; }
	public QuestionResultDto Question { get; set; }
	public Quizzes.QuizResultDto Quiz { get; set; }
	public Users.QuizResultDto User { get; set; }
}
