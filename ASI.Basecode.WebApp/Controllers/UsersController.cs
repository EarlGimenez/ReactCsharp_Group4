using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// 
        /// Get all users
        /// 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllUsers()
        {
            try
            {
                _logger.LogInformation("GetAllUsers called");
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllUsers");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Get user by ID
        /// 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<UserViewModel> GetUserById(Guid id)
        {
            try
            {
                _logger.LogInformation($"GetUserById called with id: {id}");
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUserById for {id}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Get user by email (for login)
        /// 
        [HttpGet("by-email")]
        [AllowAnonymous]
        public ActionResult<UserViewModel> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                _logger.LogInformation($"GetUserByEmail called with email: {email}");
                var user = _userService.GetUserByEmail(email);
                if (user == null)
                {
                    return NotFound(new { message = "No user found with that email address." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUserByEmail for {email}");
                return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        /// 
        /// Get user by username
        /// 
        [HttpGet("by-username")]
        [AllowAnonymous]
        public ActionResult<UserViewModel> GetUserByUsername([FromQuery] string username)
        {
            try
            {
                _logger.LogInformation($"GetUserByUsername called with username: {username}");
                var user = _userService.GetUserByUsername(username);
                if (user == null)
                {
                    return NotFound(new { message = "No user found with that username." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetUserByUsername for {username}");
                return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }

        /// 
        /// Register a new user
        /// 
        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<UserViewModel> Register([FromBody] RegisterUserViewModel model)
        {
            try
            {
                _logger.LogInformation($"Register called for email: {model?.Email}");
                
                if (model == null)
                {
                    _logger.LogWarning("Register model is null");
                    return BadRequest(new { message = "Invalid registration data" });
                }

                _logger.LogInformation("Checking if email exists...");
                // Check if email already exists
                var existingUserByEmail = _userService.GetUserByEmail(model.Email);
                if (existingUserByEmail != null)
                {
                    _logger.LogWarning($"Email {model.Email} already registered");
                    return BadRequest(new { message = "This email is already registered." });
                }

                _logger.LogInformation("Checking if username exists...");
                // Check if username already exists
                var existingUserByUsername = _userService.GetUserByUsername(model.Username);
                if (existingUserByUsername != null)
                {
                    _logger.LogWarning($"Username {model.Username} already taken");
                    return BadRequest(new { message = "This username is already taken." });
                }

                _logger.LogInformation("Creating user...");
                var user = _userService.CreateUser(model);
                _logger.LogInformation($"User created successfully with ID: {user.UserId}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Register");
                _logger.LogError($"Exception type: {ex.GetType().Name}");
                _logger.LogError($"Exception message: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { 
                    message = "Registration failed", 
                    error = ex.Message,
                    innerError = ex.InnerException?.Message,
                    type = ex.GetType().Name
                });
            }
        }

        /// 
        /// Update an existing user
        /// 
        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult UpdateUser(Guid id, [FromBody] UserViewModel model)
        {
            try
            {
                _logger.LogInformation($"UpdateUser called for id: {id}");
                model.UserId = id;
                _userService.UpdateUser(model);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in UpdateUser for {id}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Delete a user
        /// 
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult DeleteUser(Guid id)
        {
            try
            {
                _logger.LogInformation($"DeleteUser called for id: {id}");
                _userService.DeleteUser(id);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteUser for {id}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Login user
        /// 
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<UserViewModel> Login([FromBody] LoginUserViewModel model)
        {
            try
            {
                _logger.LogInformation($"Login attempt for email: {model?.Email}");
                var user = _userService.AuthenticateUser(model.Email, model.Password);
                if (user == null)
                {
                    _logger.LogWarning($"Login failed for {model?.Email}");
                    return Unauthorized(new { message = "Invalid email or password." });
                }
                _logger.LogInformation($"Login successful for {model?.Email}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in Login for {model?.Email}");
                return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
            }
        }
    }

    public class RegisterUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }

    public class LoginUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
