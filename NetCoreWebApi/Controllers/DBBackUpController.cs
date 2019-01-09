using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppModel.BindingModels;
using AppModel.DTOs;
using AutoMapper;
using BLL.Interface;
using Component;
using Component.ResponseFormats;
using DAL.DomainModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Korsa.Controllers
{
    [Produces("application/json")]
    [Route("api/DBBackUp")]
    public class DBBackUpController : Controller
    {


        public IConfiguration _configuration { get; }
        protected readonly DataContext _dbContext;
        protected readonly IBOUser _bOUser;
        protected readonly IBODriver _bODriver;
        protected readonly IBOBackup _bOBackup;
        private readonly IHostingEnvironment _environment;


        public DBBackUpController(DataContext dataContext, IConfiguration configuration, IBOUser bOUser, IBODriver bODriver, IBOBackup bOBackup, IHostingEnvironment environment)
        {
            _dbContext = dataContext;
            _configuration = configuration;
            _bOUser = bOUser;
            _bODriver = bODriver;
            _bOBackup = bOBackup;
            _environment = environment;
        }


        [HttpGet]
        [Route("GetBackUp")]
        public async Task<IActionResult> GetBackUp()
        {
            try
            {
                BackupViewModel returnModel = new BackupViewModel();

                returnModel.BackUpString = _bOBackup.TakeBackup();
                return Ok(new CustomResponse<BackupViewModel> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


        [HttpPost]
        [Route("SetMail")]
        public IActionResult SetMail(SettingsBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mail = _bOBackup.SetMail(model);
                return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "Added successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }


        [HttpGet]
        [Route("GetMail")]
        public async Task<IActionResult> GetMail()
        {
            try
            {

                MailingDTO returnModel = new MailingDTO();

                var mail= _bOBackup.TakeBackup();
                Mapper.Map(mail, returnModel);

                return Ok(new CustomResponse<MailingDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = returnModel });

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }

        }


    }
}