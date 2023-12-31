﻿namespace Play.Commom.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString { get => $"mongodb://{Host}:{Port}"; }
    }
}
