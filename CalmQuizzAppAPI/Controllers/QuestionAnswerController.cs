using Amazon.Runtime.Internal;
using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace CalmQuizzAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionAnswerController : BaseController
    {
        [HttpGet]
        public JsonResponse GetListQuestionAnswer(string qsId, string? nameSearch)
        {
            try
            {
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionService questionService = new QuestionService();
                Question qs = questionService.GetQuestionById(qsId);
                if (qs == null) throw new Exception("không tìm thấy câu hỏi");

                return Success(questionAnswerService.GetListQuestionAnswer(qsId, nameSearch));
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResponse QuestionAnswerDelete(string? id)
        {
            try
            {
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionAnswer qsAns = questionAnswerService.GetQuestionAnswerById(id);
                if (qsAns == null) throw new Exception("Không tìm thấy câu trả lời");
                questionAnswerService.QuestionAnswerDelete(id);

                return Success();
            }
            catch
            {
                return Error();
            }
        }
        [HttpPost]
        public JsonResponse QuestionAnswerCreate(QuestionAnswer model)
        {
            try
            {
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionService questionService = new QuestionService();
                Question qs = questionService.GetQuestionById(model.QuestionId);
                if (qs == null) throw new Exception("không tìm thấy câu hỏi");
                if (string.IsNullOrEmpty(model.Description)) throw new Exception("Câu trả lời không được để trống");
                model.Description = qs.Description;
                model.QuestionAnswerId = Guid.NewGuid().ToString();

                questionAnswerService.QuestionAnswerCreate(model);
                return Success();
            }
            catch
            {
                return Error();
            }
        }
        [HttpPost]
        public JsonResponse QuestionAnswerUpdate(QuestionAnswer model)
        {
            try
            {
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionAnswer qsAns = questionAnswerService.GetQuestionAnswerById(model.QuestionAnswerId);
                if (qsAns == null) throw new Exception("Không tìm thấy câu trả lời của bạn");
                qsAns.Description = model.Description;
                questionAnswerService.QuestionAnswerCreate(model);
                return Success();
            }
            catch
            {
                return Error();
            }
        }

        [HttpPost]
        public JsonResponse GetListUserAnswerSelected(UserResultUpdateModel model)
        {
            try
            {
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionService questionService = new QuestionService();
                UserResultService userResultService = new UserResultService();
                UserResult userResult = new UserResult();
                userResult.Score = 0;
                userResult.UserResultId = Guid.NewGuid().ToString();
                userResult.UserId = model.UserId;
                userResult.TopicId = model.TopicId;
                userResult.ResultJson = JsonConvert.SerializeObject(model.ListAnswer);
                for (int i = 0; i < model.ListAnswer.Count; i++)
                {
                    Question question = questionService.GetQuestionById(model.ListAnswer[i].QuestionId);

                    QuestionAnswer correct = questionAnswerService.GetCorrectAnswer(model.ListAnswer[i].QuestionId);
                    if (model.ListAnswer[i].QuestionAnswerId == correct.QuestionAnswerId)
                    {
                        userResult.Score += question.Score.Value;
                    }

                }

                userResultService.UserResultCreate(userResult);



                return Success(userResult.Score);
            }
            catch
            {
                return Error();
            }
        }

    }
}
