using CalmQuizzAppAPI.Services;
using CalmQuizzModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.IdentityModel.Tokens;

namespace CalmQuizzAppAPI.Controllers
{
   
    public class UserController : BaseController
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
        public JsonResponse GetListUser (string? nameSearch)
        {
            try
            {

                UserService userService = new UserService();// khai báo sẻvice
               
                return Success(userService.GetListUser(nameSearch));
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResponse UserUpdate (User model)
        {
            try {
                UserService userService = new UserService();
                User userCheck = userService.GetUserByName(model.Acount);
                if (userCheck == null) throw new Exception("Tài khoản không tồn tại");
                if (string.IsNullOrEmpty(model.Password)) throw new Exception("Mật khẩu không được để trống");
                userCheck.Name = model.Name;
                userCheck.Password = model.Password;
                userService.UserUpdate(userCheck);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        [HttpGet]
        public JsonResponse UserDelete (string id)
        {
            try
            {
                UserService userService = new UserService();
                User userCheck = userService.GetUserById(id);
                if (userCheck == null) throw new Exception("Tài khoản không tồn tại");
                userService.UserDelete(id);
                return Success();
            }
            catch
            {
                return Error();
            }
        }
    }
}
