﻿using FlowersBEWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowersBEWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserBase> Users { get; set; } 
    }
}
