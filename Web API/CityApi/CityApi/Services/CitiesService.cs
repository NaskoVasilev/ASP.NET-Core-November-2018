using CityApi.Data;
using CityApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityApi.Services
{
    public class CitiesService
    {
        private readonly CitiesDbContext context;

        public CitiesService(CitiesDbContext context)
        {
            this.context = context;
        }

        public CityInfo GetById(int id)
        {
            return context.Cities.Find(id);
        }

        public IEnumerable<CityInfo> GetAll()
        {
            return this.context.Cities.ToList();
        }

        public void Update(CityInfo city)
        {
            this.context.Entry(city).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Delete(CityInfo city)
        {
            this.context.Cities.Remove(city);
            context.SaveChanges();
        }

        public CityInfo Create(CityInfo city)
        {
            this.context.Cities.Add(city);
            this.context.SaveChanges();
            return city;
        }
    }
}
