﻿namespace XProxy.Domain;

/// <summary>
/// Element of list, for rendering on index
/// </summary>
public class UserSettingsItem
{
    public long Id { get; set; }

    public long UpdateInterval { get; set; }

    public string Av100Token { get; set; }

    public string XLombardAPIUrl { get; set; }

    public string XLombardToken { get; set; }
}