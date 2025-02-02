﻿using ShieldJWTLib.Models;

namespace ShieldJWTLib.Interfaces
{
    public interface IShieldUser
    {
        ShieldReturnType Create(CreateUser newUser);
        ShieldReturnType ChangePassword(string email, string newPassword);
        ShieldReturnType ConfirmPassword(string email, string confirmationCode);
    }
}
