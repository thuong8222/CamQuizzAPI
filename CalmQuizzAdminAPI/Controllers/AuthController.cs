using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace CalmQuizzAdminAPI.Controllers
{
    public class AuthController : BaseAdminAPIController
    {
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
        [HttpPost]
        public JsonResponse UserLogin(User model)
        {
            try
            {
                UserService userService = new UserService();// khai báo sẻvice

                User userCheck = userService.GetUserByName(model.Acount); //lấy từ trong csdl

                if (string.IsNullOrEmpty(model.Acount)) throw new Exception("Tên người dùng không được để trống");
                if (string.IsNullOrEmpty(model.Password)) throw new Exception("Mật khẩu không được để trống");
                if (userCheck == null) throw new Exception("Thông tin đăng nhập không chính xác1");
                if (model.Password != userCheck.Password) throw new Exception("Thông tin đăng nhập không chính xác2");

                return Success(userCheck.UserId);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        }

        [HttpGet]
        public JsonResponse ForgetPassword (User model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Acount)) throw new Exception("Tên người dùng không được để trống");
                UserService userService = new UserService();
                User userCheck = userService.GetUserByName(model.Acount);
                
              
                if (userCheck == null) throw new Exception("Thông tin đăng nhập không chính xác1");

                return Success(userCheck.UserId);
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
