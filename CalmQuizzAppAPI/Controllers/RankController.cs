using CalmQuizzAppAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CalmQuizzModels;

namespace CalmQuizzAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RankController : BaseController
    {
        [HttpGet]
        public JsonResponse GetListRank(string? nameSearch)
        {
            try 
            {
                RankService rankService = new RankService();
               
                return Success(rankService.GetListRank(nameSearch)); 
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpPost]
        public JsonResponse RankCreate(Rank model)
        {
            try
            {
                RankService rankService = new RankService();
               // Rank rank = rankService.GetListRankById(model.RankId);
                Rank rankName = rankService.GetListRankByName(model.Name);
                model.RankId = Guid.NewGuid().ToString();
                if (rankName != null) throw new Exception("Rank đã tồn tại");
                if (string.IsNullOrEmpty(model.Name)) throw new Exception("Tên Rank không được để trống");
                if (model.ScoreNeed==null) throw new Exception("Điểm không được để trống");
                rankService.RankCreate(model);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}
