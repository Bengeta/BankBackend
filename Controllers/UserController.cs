using a;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTBlockBackend.Interfaces;
using VTBlockBackend.Models;
using VTBlockBackend.Controllers;
using VTBlockBackend.Requests;
using VTBlockBackend.Responses;

namespace VTBlockBackend.Controllers;

[ApiController]
[Authorize]
public class UserController:BaseController
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [AllowAnonymous]
    [HttpPost("api/auth/signup")]
    public async Task<ResponseModel<string>> SignUp(SignUpRequest request)
    {
        return await _userService.SignUp(request.name, request.password, request.email);
    }

    [AllowAnonymous]
    [HttpPost("auth/login")]
    public async Task<ResponseModel<string>> Login(LoginRequest loginRequest)
    {
        return await _userService.Login(loginRequest.email, loginRequest.password);
    }

    [HttpGet("user")]
    public async Task<ResponseModel<UserModelResponse>> GetUser()
    {
        var token = Token();
        return await _userService.GetUser(token);
    }

    [HttpPost("/device")]
    public async Task<ResponseModel<string>> AddDevice(AddTokenDeviceRequest request)
    {
        var token = Token();
        return await _userService.AddDevice(token, request.DeviceToken);
    }

    [HttpPut("edite/password")]
    public async Task<ResponseModel<string>> EditePassword(EditPasswordRequest request)
    {
        var token = Token();
        return await _userService.EditePassword(token, request.old_password, request.new_password);
    }


    [HttpPut("/edite/username")]
    public async Task<ResponseModel<string>> EditeUsername([Bind("User")] EditUsernameRequest request)
    {
        var token = Token();
        return await _userService.EditeUsername(token, request.new_username);
    }
}