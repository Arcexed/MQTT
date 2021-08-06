﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MQTTWebApi.Models;


namespace MQTTWebApi.Static
{
    public class MqttDBContext : DbContext
    {
        public DbSet<Measurements> Measurements { get; set; }
        public DbSet<Device> Device  { get; set; }
        public DbSet<Events> Events { get; set; }

        public MqttDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSql(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");

        }

        private string GetConfigurationString()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
