﻿namespace Chat.Commons.Models;

public class AuthResponse
{
    public User User { get; set; }
    public string Token { get; set; }
    public AuthResponse(User user, string token)
    {
        User = user;
        Token = token;
    }
}
