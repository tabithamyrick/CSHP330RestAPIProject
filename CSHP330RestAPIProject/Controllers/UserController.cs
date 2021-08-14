using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CSHP330RestAPIProject.Models;
using CSHP330RestAPIProject.Repository;
using Newtonsoft.Json;
using CSHP330RestAPIProject.Services;


namespace CSHP330RestAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            if(repository.UserList == null)
            {
                repository.UserList = new List<User>();
            }
            this._repository = repository;
        }

        [HttpGet]
        public string Get()
        {
            if (_repository.UserList != null)
            {
                return JsonConvert.SerializeObject(_repository.UserList);
            }
            else
            {
                return "No Users Found";
            }
           
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            try
            {
                User GetUser = _repository.UserList.Where(x => x.Id == id).FirstOrDefault();
                return JsonConvert.SerializeObject(GetUser); ;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return "Oops, Somethigng Went Wrong, Please Try Again";
            }
       
        }

        [HttpPost]
        public string Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                var userList = _repository.UserList;
                var hashedPwd = new HashPasswordService();
                user.CreatedDate = DateTime.Now;
                user.Id = userList.Count + 1;
                user.UserPassword = hashedPwd.EncryptPassword(user.UserPassword, user.UserEmail);
                userList.Add(user);
                return "User Successfully Added. The Users UserID is " + user.Id;
            }
            else
            {
                return "Something Went Wrong, Please Check Your Entries and Try Again.";
            }
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] User user)
        {
            try
            {
                var hashedPwd = new HashPasswordService();
                User UserToUpdate = _repository.UserList.Where(x => x.Id == id).FirstOrDefault();
                UserToUpdate.UserEmail = user.UserEmail;
                UserToUpdate.UserPassword = hashedPwd.EncryptPassword(user.UserPassword, user.UserEmail);

                return "Successfully Updated User";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return "Failed to Update User";
            }
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                var deletedUser = _repository.UserList.Where(x => x.Id == id).FirstOrDefault();
                _repository.UserList.Remove(deletedUser);
                return "User Successfully Deleted";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return "Failed to Delete User";
            }
            
            
        }
    }
}
