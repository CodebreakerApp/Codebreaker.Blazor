using BlazorMM.Server.Services;
using BlazorMM.Shared;
using BlazorMM.Shared.APIModels;

using Microsoft.AspNetCore.Mvc;

namespace BlazorMM.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MasterMindController : ControllerBase
{
    private readonly GameService _gameService;
    public MasterMindController(GameService gameService)
    {
        _gameService = gameService;
    }

    // GET: api/<MasterMindController>/name=christian
    [HttpPost("start")]
    public async Task<ActionResult<CreateGameResponse>> CreateGame(CreateGameRequest req)
    {
        using BlazorMM.Shared;
        using System.Collections.Concurrent;
    }

    [HttpPost("move")]
    public async Task<ActionResult<MoveResponse>> Move(MoveRequest request)
    {
        GameMove move = new(request.Id, request.MoveNumber, request.CodePegs.ToList());
        var result = await _gameService.SetMoveAsync(move);
        MoveResponse response = new(result.GameId, result.Completed, result.Won, result.KeyPegs);
        return Ok(response);
    }
}
