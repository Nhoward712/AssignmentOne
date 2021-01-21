using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AssignmentOne
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }

        public void AddLocation(Location loc){
            this.Locations.Add(loc);
            this.SaveChanges();

        }

        public void AddEvent(Event evt)
        {
            this.Events.Add(evt);
            this.SaveChanges();
        }

        public void ListEvents(){
            var db = new EventContext();
            var locations = db.Locations;

            var query = Events.OrderBy(e => e.EventId);

            Console.WriteLine("All Events in the database:");
            Console.WriteLine("{0,-6}{1,-30}{2,-30}", "Event Id", "Event time", "Event Location");
            Console.WriteLine("---------------------------------------------------");
            foreach (var item in query)
            {
                var locQuery = locations.FirstOrDefault(l => l.LocationId == item.LocationId);
                Console.WriteLine("{0,-6}{1,-30}{2,-30}", item.EventId, item.TimeStamp, locQuery.Name);
                Console.WriteLine();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["EventContext:ConnectionString"]);
        }
    }
}