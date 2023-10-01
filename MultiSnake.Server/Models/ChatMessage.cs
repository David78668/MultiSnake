using System;
using System.Collections.Generic;

namespace MultiSnake.Server.Models;

public partial class ChatMessage
{
    public int Id { get; set; }

    public string? User { get; set; }

    public string? Message { get; set; }

    public string? Time { get; set; }
}
