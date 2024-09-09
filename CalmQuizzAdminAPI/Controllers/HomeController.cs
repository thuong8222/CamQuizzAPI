using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;


namespace CalmQuizzAdminAPI.Controllers
{
 
    public class HomeController : BaseAdminAPIController
    {
        [HttpGet]
        public int Test (int a, int b)
        {
            return ( a+b);
        }

        [HttpPost]
        public JsonResponse UserRegister(User model)
        {
            try
            {
                UserService userService = new UserService();// khai báo sẻvice
                model.UserId = Guid.NewGuid().ToString();
                model.Token = Guid.NewGuid().ToString();
                User userCheck = userService.GetUserByName(model.Acount); //lấy từ trong csdl
                if (userCheck != null) throw new Exception("Tên người đùng đã tồn tại");

                if (string.IsNullOrEmpty(model.Acount)) throw new Exception("Tên người dùng không được để trống");
                if (string.IsNullOrEmpty(model.Name)) throw new Exception("Tên đầy đủ không được để trống");
                if (string.IsNullOrEmpty(model.Password)) throw new Exception("Mật khẩu không được để trống");

                model.TotalScore = 0;
                model.RankId = 0;

                userService.UserRegister(model);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
    

}
