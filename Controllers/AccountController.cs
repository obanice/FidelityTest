using FidelityTest.DB;
using FidelityTest.Model;
using FidelityTest.Repository;
using FidelityTest.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//[Route("Users/{accountId}")]
namespace FidelityTest.Controllers
{
    //[Authorize]
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly AppDbContext _context;
        private readonly IJWTManagerRepository  _jWTManager;
        public AccountController(AppDbContext context, IJWTManagerRepository jWTManager)
        {
            _context = context;
            this._jWTManager = jWTManager;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserAccess userdata) 
        {
            var token = _jWTManager.Authenticate(userdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }


        /// <summary>
        /// Get all account in DB
        /// </summary>
        /// <returns>List of accounts </returns>
       [HttpGet]       
        public IActionResult Accounts()
        {
            try
            {
                var allAccount = _context.Accounts.Where(x => x.Id != 0).ToList();

                if (allAccount.Count > 0)
                {
                    return Ok(allAccount);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        [HttpGet("{id}")]
        public IActionResult Account(int id)
        {
            try
            {
                var account = _context.Accounts.Where(x => x.Id == id).FirstOrDefault(); 
               
                if (account != null)
                {
                    var allUsers = _context.UserDetails.Where(x => x.AccountId == id).ToList();

                    var accountInfo = new AccountsViewModel()
                    {
                        Id = account.Id,
                        CompanyName = account.CompanyName,
                        Website = account.Website,
                        Users = allUsers
                    };
                    return Ok(accountInfo);
                }
                return BadRequest("Account not found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Posting to Db
        [HttpPost]
        public IActionResult Post([FromForm] UpdateAccountViewModel account)
        {
            if (account.CompanyName == null)
            {
                return BadRequest("Company name is required");
            }

            try
            {
                var allAccounts = _context.Accounts.Where(x => x.Id != 0).ToList();
                var checkName = allAccounts.Find(x => x
                .CompanyName == account.CompanyName);
                if (checkName != null)
                {
                    return BadRequest("Company name exist");
                }

                //foreach (var allAccount in allAccounts)
                //{
                //    if (allAccount.CompanyName == account.CompanyName)
                //    {
                //        return BadRequest("Company name exist");
                //    }
                //}

                var accountInfo =  new Account()
                {                   
                    CompanyName = account.CompanyName,
                    Website = account.Website
                };
                _context.Add(accountInfo);
                _context.SaveChanges();
                
                return Ok(accountInfo);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //Delete account using 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var account = _context.Accounts.Where(x => x.Id == id).FirstOrDefault();
                if (account != null)
                {
                    var allUsers = _context.UserDetails.Where(x => x.AccountId == id).ToList();
                    if (allUsers.Count() > 0)
                    {
                        _context.RemoveRange(allUsers);
                        _context.SaveChanges();

                    }

                    _context.Remove(account);
                    _context.SaveChanges();

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] UpdateAccountViewModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateAccountInfo = _context.Accounts.Where(x => x.Id == id).FirstOrDefault();
                    if (updateAccountInfo != null)
                    {
                        updateAccountInfo.CompanyName = account.CompanyName;
                        updateAccountInfo.Website = account.Website;
                       
                        _context.Update(updateAccountInfo);
                        _context.SaveChanges();

                        return Ok(updateAccountInfo);
                    }
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }


        //User Detail


        [HttpPost("{accountId}/user")]
        public IActionResult AddUser(int accountId, [FromBody] UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkExistingAccount = _context.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
                    if (checkExistingAccount == null)
                    {
                        return BadRequest("Account not found");
                    }
                    var checkExistingEmail = _context.UserDetails.Where(x => x.Email == user.Email).FirstOrDefault();
                    if (checkExistingEmail != null)
                    {
                        return BadRequest("Email already belongs to another user");
                    }

                    var userInfo = new UserDetail()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        AccountId = accountId,
                    };
                    _context.Add(userInfo);
                    _context.SaveChanges();

                    return Ok(userInfo);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet("{id}/users")]        
        public IActionResult GetAllUser(int id)
        {
            try
            {
                var allUser = _context.UserDetails.Where(u => u.Id != 0 && u.AccountId == id).Include(a => a.Accounts).ToList();

                if (allUser.Count > 0)
                {
                    return Ok(allUser);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id}/{userId}")]
        public IActionResult GetUser(int id, int userId)
        {
            try
            {
                var user = _context.UserDetails.Where(x => x.Id == userId && x.AccountId == id).FirstOrDefault();

                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        [Route("{id}/users/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var user = _context.UserDetails.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();

                    return Ok("Deleted successfully");
                }
                return BadRequest("No user found with such identity");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}/users/{userId}")]
        public IActionResult UpdateUserInfo(int userId, [FromBody] UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getUser = _context.UserDetails.Where(x => x.Id == userId).FirstOrDefault();
                    if (getUser != null)
                    {
                        getUser.FirstName = user.FirstName;
                        getUser.LastName = user.LastName;
                        getUser.Email = user.Email;

                        _context.Update(getUser);
                        _context.SaveChanges();

                        return Ok(getUser);
                    }
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}




                            //[HttpGet]
                            //[Route("{test}/user")]
                            //public List<string> Get()
                            //{
                            //    var users = new List<string>
                            //    {
                            //        "Satinder Singh",
                            //        "Amit Sarna",
                            //        "Davin Jon"
                            //    };
                            //    return users;
                            //}