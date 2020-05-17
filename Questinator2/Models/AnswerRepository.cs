using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Questinator2.Models
{
    public interface IAnswerRepository
    {
        void Create(Answer answer);
        void Delete(int id);
        Answer Get(int id);
        List<Answer> GetByUserId(int id);
        List<Answer> GetByQuestionId(int id);
        List<Answer> GetAnswers();
        void Update(Answer answer);
    }
    public class AnswerRepository: IAnswerRepository
    {
        string connectionString = null;

        public AnswerRepository(string conn)
        {
            connectionString = conn;
        }

        public List<Answer> GetAnswers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                List<Answer> answers = db.Query<Answer>("SELECT * FROM [Answers]").ToList();

                return answers;
            }
        }

        public Answer Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Answer>("SELECT * FROM [Answers] WHERE [AnswerId] = @id", new { id }).FirstOrDefault();
            }
        }
        //++
        public List<Answer> GetByUserId(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Answer>("SELECT * FROM [Answers] WHERE [UserOwnerId] = @id", new { id }).ToList();
            }
        }
        //++
        public List<Answer> GetByQuestionId(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Answer>("SELECT * FROM [Answers] WHERE [QuestionId] = @id", new { id }).ToList();
            }
        }

        public void Create(Answer answer)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO [Answers] (AnswerMsg, QuestionId, UserOwnerId)" +
                    " VALUES(@AnswerMsg, @QuestionId, @UserOwnerId)";
                db.Execute(sqlQuery, answer);
            }
        }

        public void Update(Answer answer)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE [Answers] SET AnswerMsg = @AnswerMsg, QuestionId = @QuestionId, UserOwnerId = @UserOwnerId WHERE AnswerId = @AbswerId";
                db.Execute(sqlQuery, answer);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM [Answers] WHERE AnswerId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }


    }
}
