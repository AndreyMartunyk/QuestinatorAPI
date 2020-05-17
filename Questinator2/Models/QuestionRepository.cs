using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Questinator2.Models
{
    public interface IQuestionRepository
    {
        void Create(Question question);
        void Delete(int id);
        Question Get(int id);
        List<Question> GetQuestions();
        void Update(Question question);
    }
    public class QuestionRepository : IQuestionRepository
    {
        string connectionString = null;

        public QuestionRepository(string conn)
        {
            connectionString = conn;
        }

        public List<Question> GetQuestions()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                List<Question> questions =  db.Query<Question>("SELECT * FROM [Questions]").ToList();
                
                foreach (var question in questions)
                {
                    question.Answers = db.Query<Answer>("SELECT AnswerId, AnswerMsg, UserOwnerId  FROM [Answers] WHERE QuestionId = @QuestionId", new { question.QuestionId }).ToList();
                }
                return questions;
            }
        }


        public Question Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Question>("SELECT * FROM [Questions] WHERE [QuestionId] = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Question question)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO [Questions] (UserOwnerId, QuestionMessage, CreateDate, MaxCustomQuestions, MaxAnswers, IsSaved, IsActive, AnswerDeadline )" +
                    " VALUES(@UserOwnerId, @QuestionMessage, @CreateDate, @MaxCustomQuestions, @MaxAnswers, @IsSaved, @IsActive, @AnswerDeadline)";
                db.Execute(sqlQuery, question);

                foreach (var answer in question.Answers)
                {
                    //добавление ответов на этот вопрос
                    sqlQuery = "INSERT INTO [Answers] (AnswerMsg, QuestionId, UserOwnerId)" +
                    " VALUES(@AnswerMsg, @QuestionId, @UserOwnerId)";
                    db.Execute(sqlQuery, answer);
                }
                
            }
        }

        public void Update(Question question)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE [Questions] SET " +
                    "UserOwnerId = @UserOwnerId, " +
                    "QuestionMessage = @QuestionMessage, " +
                    "CreateDate = @CreateDate, " +
                    "MaxCustomQuestions = @MaxCustomQuestions, " +
                    "MaxAnswers = @MaxAnswers, " +
                    "IsSaved = @IsSaved, " +
                    "IsActive = @IsActive, " +
                    "AnswerDeadline = @AnswerDeadline  " +
                    "WHERE QuestionId = @QuestionId";
                db.Execute(sqlQuery, question);

                foreach (var answer in question.Answers)
                {
                    sqlQuery = "UPDATE [Answers] SET AnswerMsg = @AnswerMsg, QuestionId = @QuestionId, UserOwnerId = @UserOwnerId WHERE AnswerId = @AbswerId";
                    db.Execute(sqlQuery, answer);
                }
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM [Questions] WHERE QuestionId = @id";
                db.Execute(sqlQuery, new { id });

                //удаление ответов на этот вопрос
                sqlQuery = "DELETE FROM [Answers] WHERE QuestionId = @id";
                db.Execute(sqlQuery, new { id });
                
            }
        }

    }
}
