using System;
using System.Linq;


namespace AssignmentOne
{
    public class SeedData
    {
        
        public void seedLocations()
        {
            var db = new EventContext();
            Console.WriteLine("Initialize Locations(Y or N): "); 
                string ans = Console.ReadLine();
                if(ans == "Yess")
                {
                    string [] locationList = {"Pluto","Mars","Venus","Jupiter","Mercury","Earth","Saturn","Neptune"};
                    
                    for(int i = 0; i < 7; i++){
                        Console.WriteLine("Location {0,G} Added",locationList[i]);
                     var loc = new Location { Name = locationList[i] };
                     db.AddLocation(loc);
                    }
                }
        }

        public void seedEvents()
        {
            //Create events for past 6 months
            //1-5 events per day
            //Random locations

            //create 6 months of days
            var rand = new Random();
            var db = new EventContext();
            DateTime date = DateTime.Now;

            Console.WriteLine("Seed Event Data? (Y or N)");
            string seed = Console.ReadLine();
            if(seed == "Y")
            {
                for(int i = -180; i < 0; i++)
                {
                    //create random number of events per day
                    int days = rand.Next(1,5);
                    for (int ii = 0; ii < days; ii++){
                        //create random time stamp for each day
                        DateTime newDate = date.AddDays(i);
                        int hours = rand.Next(-11,13);
                        int minutes = rand.Next(-30,30);
                        int seconds = rand.Next(-30,30);
                        DateTime newHours = newDate.AddHours(hours);
                        DateTime newMinutes = newHours.AddMinutes(minutes);
                        DateTime time = newMinutes.AddSeconds(seconds);
                        
                        //pick random event
                        var eventLocation = GetRandomLocation(db);
                        var evt = new Event {TimeStamp = time, Flagged = false, LocationId = eventLocation.LocationId};
                        Console.WriteLine("Event Added - {0,30}{1,20}",evt.TimeStamp, evt.LocationId);
                        db.AddEvent(evt);
                    }
                }
            }
        }
        public static Location GetRandomLocation(EventContext db)
        {
            var randomLocationId = new Random();
            int id = randomLocationId.Next(1,8);
            if (id > 0 && id < 9)
            {
                Location loc = db.Locations.FirstOrDefault(l => l.LocationId == id);
                if (loc != null)
                {
                    return loc;
                }
            }
            return null;
        } 



    }



}