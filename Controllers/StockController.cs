using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VTBlockBackend.Controllers;


[ApiController]
[Authorize]
public class StockController:BaseController
{
    
}