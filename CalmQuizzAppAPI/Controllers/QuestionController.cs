using Amazon.Runtime.Internal.Transform;
using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CalmQuizzAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : BaseController
    {
        [HttpGet]
        public JsonResponse GetListQuestion(string? topicId)
        {
            try
            {
                QuestionService questionService = new QuestionService();
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                TopicService topicService = new TopicService();
                Topic topic = topicService.GetTopicById(topicId);
                if (topic == null) throw new Exception("Chủ đề không tồn tại");
                return Success(new
                {
                    listQuestion= questionService.GetListQuestion(topicId),
                    listQuestionAnswer= questionAnswerService.GetListQuestionAnswerByTopicId(topicId)
                });

            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse QuestionCreate(Question model)
        {
            try
            {
                QuestionService questionService = new QuestionService();
                TopicService topicService =new TopicService();
                Topic topic = topicService.GetTopicById(model.TopicId);
                if (topic == null) throw new Exception("Chủ đề không tồn tại");
                model.QuestionId = Guid.NewGuid().ToString();
                model.TopicId = topic.TopicId;
                
                if (string.IsNullOrEmpty(model.Description)) throw new Exception("Mô tả câu hỏi không được để trống");
                if (model.Score == null) throw new Exception("Điểm câu hỏi không được để trống");
                questionService.QuestionCreate(model);
                model.Score = 0;
                QuestionAnswerService questionAnswerService = new QuestionAnswerService();
                QuestionAnswer qsAns = questionAnswerService.CountQuestionAnswer();
         
                int quantity = 1;
                topicService.UpdateToltalQuestionTopic(model.TopicId, quantity);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse QuestionUpdate(Question model)
        {
            try
            {
                QuestionService questionService = new QuestionService();
                Question qs = questionService.GetQuestionById(model.QuestionId);
                    qs.Description = model.Description;
                    qs.Score = model.Score;
                    qs.Image = model.Image;
                questionService.QuestionUpdate(model.QuestionId);
                return Success();

            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
