﻿using AutoMapper;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Carpool.Data.Models;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Carpool.Data
{
    public class Repository
    {
        public CarpoolDbContext dbContext;
        public Repository()
        {
            dbContext = new CarpoolDbContext();
        }

        public void Add<T>(T tObject) where T : class
        {
            //try
            //{
                dbContext.Entry(tObject).State = EntityState.Detached;
                dbContext.Set<T>().Add(tObject);
                dbContext.SaveChanges();
            //}
            //catch (DbUpdateException e)
            //{
            //    throw new Exception(e.Message);
            //}
        }


        public List<T> GetTable<T>() where T : class
        {
            return dbContext.Set<T>().ToList<T>();
        }

        public List<T> GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return dbContext.Set<T>().Where(predicate.Compile()).ToList<T>();
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().FirstOrDefault(predicate.Compile());
        }

        public void Update<T>(T tObject) where T : class
        {
            dbContext.Update(tObject);
            dbContext.SaveChanges();
            dbContext.Entry(tObject).State = EntityState.Detached;
        }

        public int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return dbContext.Set<T>().Where(predicate.Compile()).Count();
        }

        public void Delete<T>(T tObject) where T : class
        {
            try
            {
                dbContext.Set<T>().Remove(tObject);
                dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
//Scaffold-DbContext "Server=(localdb)\MSSQLLocalDb;Database=Carpool;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
