﻿namespace Domain
{
    public class AppConfiguration
    {
        public string? AllowedHosts { get; set; }
        public bool AllowAnyMethod { get; set; }
        public double JWTduration { get; set; }

    }
}
