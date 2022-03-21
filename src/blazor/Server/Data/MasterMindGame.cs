﻿namespace BlazorMM.Server.Data;

public record MasterMindGame(string MasterMindGameId, string Code, string User, DateTime Time)
{
    public List<MastermindMove> Moves { get; init; } = new List<MastermindMove>();
}

public record MastermindMove(int MoveNumber, string Moves, string Keys);

public record MasterMindGameMove(string Id, string MasterMindGameId, int MoveNumber, string Move, DateTime Time, string Keys, string Code);
