using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.AggregatesModel.Auth;
using Portal.Domain.Enumerations;
using Portal.Domain.Model.Auth;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;

namespace Portal.API.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;

        public TestController(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("user")]
        public IActionResult CreateNewUser()
        {
            var newUser = new User()
            {
                Username = "staff",
                Password = "staff",
                Type = UserTypeEnum.Staff
            };
            this.unitOfWork.UserRepository.Create(newUser);
            this.unitOfWork.SaveChanges();
            return SuccessResult("");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("user")]
        public IActionResult GetAllUser()
        {
            var users = this.unitOfWork.UserRepository.GetAll();
            var usersModel = users.Adapt<List<UserModel>>();
            return SuccessResult(usersModel);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("user/{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] UserUpdateModel model)
        {
            var user = this.unitOfWork.UserRepository.GetById(id);
            if (user == null)
                return ErrorResult("no.user.found");
            user.Username = model.Username;
            user.Password = model.Password;
            user.Type = model.Type;
            user.UpdatedDate = DateTime.Now;
            this.unitOfWork.UserRepository.Update(user);
            this.unitOfWork.SaveChanges();
            return SuccessResult(user);
        }
    }
}