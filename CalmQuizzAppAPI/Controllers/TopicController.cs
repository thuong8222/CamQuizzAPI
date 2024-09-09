using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalmQuizzAppAPI.Controllers
{

    [ApiController]
    public class TopicController : BaseController
    {

        [HttpGet]
        public JsonResponse GetListTopic(string? nameSearch)
        {
            try
            {
                TopicService topicService = new TopicService();
              
                return Success(topicService.GetListTopic(nameSearch));
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse TopicCreate(Topic model)
        {
            try
            {
                TopicService topicService = new TopicService();
                Topic topicName = topicService.GetListTopicByName(model.TopicName);
                model.TopicId = Guid.NewGuid().ToString();
                model.ToltalQuestion = 0;
                model.TotalTime = 0;
              
                topicService.CreateTopic(model);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}
