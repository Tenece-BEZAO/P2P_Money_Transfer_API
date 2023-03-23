﻿namespace peer_to_peer_money_transfer.DAL.Enums;

public enum UserType
{
    Indiviual = 1,
    Corporate,
    Admin,
}

public static class UserTypeExtension
{
    public static string? GetStringValue(this UserType userType)
    {
        return userType switch
        {
            UserType.Indiviual => "Indiviual",
            UserType.Corporate => "Corporate",
            UserType.Admin => "Admin",
            _ => null
        };
    }
}