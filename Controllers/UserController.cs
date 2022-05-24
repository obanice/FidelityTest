using FidelityTest.DB;
using FidelityTest.Model;
using FidelityTest.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FidelityTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        //UserDetail action

       

    }
}

