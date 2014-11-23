using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SOFT331_Assignment.Models
{
    public class DatabaseInitialiser : CreateDatabaseIfNotExists<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {
            var fareTypes = new List<FareType> {           
                new FareType { FareTypeDescription = "Adult"},
                new FareType { FareTypeDescription = "Over 60"},
                new FareType { FareTypeDescription = "Child"}
            };
            fareTypes.ForEach(f => context.FareTypes.Add(f));
            context.SaveChanges();

            var eventTypes = new List<EventType> {
                new EventType { EventName="Full Line Return & Day Rover", EventDescription="Full Line Return & Day Rover"},
                new EventType { EventName="Trwyn to Dolgoch Return", EventDescription="Trwyn to Dolgoch Return"},
                new EventType { EventName="Other Tickets", EventDescription="Other Tickets"}
            };
            eventTypes.ForEach(e => context.EventTypes.Add(e));
            context.SaveChanges();

            var stations = new List<Station>{
                new Station { StationName="Tywyn Wharf", StationDescription="Wharf is the railway's main terminal station and is where most passengers join the train. It is conveniently situated for the railway car park, bus services, and Tywyn's mainline station, served by Arriva Trains Wales. (See how to get to the railway.) Station facilities include toilets (including disabled toilet), refreshments, Railway Shop and the Narrow Gauge Railway Museum."},
                new Station { StationName="Pendre", StationDescription="Pendre is a small station serving the eastern end of Tywyn. It is also the operational centre of the railway. Here are situated the locomotive and carriage sheds together with the engineering workshops. Request stop."},
                new Station { StationName="Rhydyronen", StationDescription="A charming little station in delightful surroundings. As well as serving the local hamlet and adjoining caravan and camping park, the station is a good starting point for mountain walks. Request stop."},
                new Station { StationName="Brynglas", StationDescription="Brynglas is another small station, serving the hamlet of Pandy, and is another starting point for walks. Request stop."},
                new Station { StationName="Dolgoch Falls", StationDescription="Another station in delightful surroundings, and a popular place for passengers to stop off awhile to visit the Falls. There is easy access from the station to the Falls area where up to two hours can be spent in exploration. Why not break your journey here, train service permitting? There is a pay and display car park nearby, adjacent to the B4405 road, and there are picnic tables in the station area. Environmentally friendly toilet facilities are provided. Passengers should note that the path to the station is not suitable for the elderly or disabled."},
                new Station { StationName="Abergynolwyn", StationDescription="Abergynolwyn is the main inland station and the starting point for many passengers as there is no road access to our terminus at Nant Gwernol. Station facilities include the Quarryman's Caban Tea Room, shop and toilets (including disabled toilet). At the foot of the (steep) station drive, adjacent to the B4405 road, there is a free car park and a new adventure playground and picnic area. Abergynolwyn is the starting point for a forest walk to Nant Gwernol."},
                new Station { StationName="Nant Gwernol", StationDescription="Nant Gwernol, situated in a natural ravine, is the eastern terminus of the railway and has NO ROAD ACCESS. Access to the station is via the forest walks or the footpath from the car park in Abergynolwyn village, both of which are unsuitable for the elderly or disabled. It is the starting point for walks which enable visitors to explore unspoilt woodland and mountain waterfalls and see something of the former slate mining activities in the area, returning to either Nant Gwernol or Abergynolwyn Stations. All walks are well signposted."},
                new Station { StationName="Request Stops", StationDescription="Trains call on request at Tywyn Pendre, Hendy Halt, Fach Goch Halt, Cynfal Halt, Rhydyronen, Tynllwyn Hen Halt, Brynglas and Quarry Siding Halt. Passengers wishing to board should give a clear handsignal to the driver, whilst those wishing to alight should inform the guard on boarding the train. Please note that car parking at most of these stations and halts is very difficult."}
            };
            stations.ForEach(s => context.Stations.Add(s));
            context.SaveChanges();
        }
    }
}