using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSHP330RestAPIProject.Models;
using CSHP330RestAPIProject.Repository;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSHP330RestAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Create Postman requests for each action(export your requests and save it to the repo)
        //You DO NOT need a database, you can store it internally in a List or Array.
        private IUserRepository _repository;

        //private readonly List<User> userList;
        public UserController(IUserRepository repository)
        {
            if(repository.UserList == null)
            {
                repository.UserList = new List<User>();
            }
            this._repository = repository;
        }

        // GET: api/<UserController>
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

        // GET api/<UserController>/5
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

        // POST api/<UserController>
        [HttpPost]
        public string Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                var userList = _repository.UserList;
                user.CreatedDate = DateTime.Now;
                user.Id = userList.Count + 1;
                userList.Add(user);
                return "User Successfully Added. The Users UserID is " + user.Id;
            }
            else
            {
                return "Something Went Wrong, Please Check Your Entries and Try Again.";
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] User user)
        {
            try
            {
                User UserToUpdate = _repository.UserList.Where(x => x.Id == id).FirstOrDefault();
                UserToUpdate.UserEmail = user.UserEmail;
                UserToUpdate.UserPassword = user.UserPassword;

                return "Successfully Updated User";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return "Failed to Update User";
            }
        }

        // DELETE api/<UserController>/5
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
