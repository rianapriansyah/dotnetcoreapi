using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using stargate.Models;
using stargate.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace stargate.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepo;

        public UsersController(UserContext context)
        {
            _userRepo = new UserRepository(context);
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userRepo.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            var exstingUser = _userRepo.GetById(id);

            if (exstingUser == null)
            {
                return NotFound();
            }

            _userRepo.Update(user, id);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            string pass = user.Password;
            user.Password = Utils.HashPash(pass);
            
            _userRepo.Add(user);

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

           _userRepo.Delete(user);

            return user;
        }

        private bool UserExists(int id)
        {
            var user =  _userRepo.GetById(id);
            return user == null ? true : false;
        }
    }
}
