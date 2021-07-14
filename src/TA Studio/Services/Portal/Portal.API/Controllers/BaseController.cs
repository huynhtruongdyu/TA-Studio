using _0.Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.API.Controllers
{
    [Authorize]
    //[ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// prepare error result
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult ErrorResult(List<string> errorMessages, List<string> localeParams = null)
        {
            var dataResult = new ErrorResponseModel
            {
                Status = false,
                ErrorMessages = errorMessages,
                ErrorCode = 0,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        /// <summary>
        /// prepare error result
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult ErrorResult(string errorMessage, List<string> localeParams = null)
        {
            var errorMessages = new List<string> { errorMessage };
            var dataResult = new ErrorResponseModel
            {
                Status = false,
                ErrorMessages = errorMessages,
                ErrorCode = 0,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        /// <summary>
        /// prepare error result
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult ErrorResult(Exception error, List<string> localeParams = null)
        {
            var errorMessages = new List<string> { error.Message };
            var dataResult = new ErrorResponseModel
            {
                Status = false,
                ErrorMessages = errorMessages,
                ErrorCode = 0,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        /// <summary>
        /// prepare success result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult SuccessResult(string message, List<string> localeParams = null)
        {
            var dataResult = new SuccessResponseModel<object>
            {
                Status = true,
                Message = message,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        /// <summary>
        /// prepare success result
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult SuccessResult(object obj, string message = "", List<string> localeParams = null)
        {
            var dataResult = new SuccessResponseModel<object>
            {
                Status = true,
                Message = message,
                Data = obj,
                LocaleParams = localeParams
            };
            return Ok(dataResult);
        }

        [NonAction]
        public IActionResult Result<T>(CombineResponseModel<T> model)
        {
            if (model.Status)
            {
                var dataResult = new SuccessResponseModel<T>
                {
                    Status = true,
                    Message = model.Message,
                    Data = model.Data,
                    LocaleParams = model.LocaleParams
                };
                return Ok(dataResult);
            }
            else
            {
                var dataResult = new ErrorResponseModel
                {
                    Status = false,
                    ErrorMessages = model.ErrorMessages,
                    ErrorCode = model.ErrorCode,
                    LocaleParams = model.LocaleParams
                };
                return Ok(dataResult);
            }
        }

        /// <summary>
        /// File Result
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult FileResult(byte[] file, string fileName, string contentType)
        {
            if (file == null) return ErrorResult("No results were found for your selections.");
            return File(file, contentType, fileName);
        }

        /// <summary>
        /// ValidModel
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public IActionResult ValidModel()
        {
            foreach (var value in ModelState.Keys.Where(value => ModelState[value].Errors.Count > 0))
            {
                return ErrorResult(!string.IsNullOrEmpty(ModelState[value].Errors[0].ErrorMessage)
                    ? ModelState[value].Errors[0].ErrorMessage
                    : ModelState[value].Errors[0].Exception.Message);
            }

            return ErrorResult("Request model invalid");
        }
    }
}