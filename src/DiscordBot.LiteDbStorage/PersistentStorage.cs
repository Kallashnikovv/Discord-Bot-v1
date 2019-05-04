﻿using LiteDB;
using DiscordBot.Core.Infrastructure;
using DiscordBot.Core.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace DiscordBot.LiteDbStorage
{
    public class PersistentStorage : IPersistentStorage
    {
        private readonly string _dbFileName;

        public PersistentStorage(IFileSystem fileSystem)
        {
            _dbFileName = Path.Combine(fileSystem.DataStoragePath, "DiscordBot.db");
        }

        public IEnumerable<T> RestoreMany<T>(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.Find(predicate);
            }
        }

        public T RestoreSingle<T>(Expression<Func<T, bool>> predicate)
            => RestoreMany(predicate).FirstOrDefault();

        public bool Exists<T>(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.Exists(predicate);
            }
        }

        public void Store<T>(T item)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                collection.Insert(item);
            }
        }

        public void Update<T>(T item)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                collection.Update(item);
            }
        }
    }
}
