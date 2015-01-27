namespace SOFT331_Assignment.Migrations
{
    using SOFT331_Assignment.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SOFT331_Assignment.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SOFT331_Assignment.Models.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            var stations = new List<Station>{
                new Station { StationID=1, StationName="Tywyn Wharf", StationDescription="Wharf is the railway's main terminal station and is where most passengers join the train. It is conveniently situated for the railway car park, bus services, and Tywyn's mainline station, served by Arriva Trains Wales. (See how to get to the railway.) Station facilities include toilets (including disabled toilet), refreshments, Railway Shop and the Narrow Gauge Railway Museum.", MainlineStationNear=false, RefreshmentsAvailable=true, RequestStop=false, ToiletAvailable=false, WheelchairAccessible=false, CarPark=false },
                new Station { StationID=2, StationName="Pendre", StationDescription="Pendre is a small station serving the eastern end of Tywyn. It is also the operational centre of the railway. Here are situated the locomotive and carriage sheds together with the engineering workshops. Request stop.", MainlineStationNear=false, RefreshmentsAvailable=false, RequestStop=true, ToiletAvailable=true, WheelchairAccessible=false, CarPark=true },
                new Station { StationID=3, StationName="Rhydyronen", StationDescription="A charming little station in delightful surroundings. As well as serving the local hamlet and adjoining caravan and camping park, the station is a good starting point for mountain walks. Request stop.", MainlineStationNear=false, RefreshmentsAvailable=false, RequestStop=true, ToiletAvailable=false, WheelchairAccessible=false, CarPark=false },
                new Station { StationID=4, StationName="Brynglas", StationDescription="Brynglas is another small station, serving the hamlet of Pandy, and is another starting point for walks. Request stop.", MainlineStationNear=false, RefreshmentsAvailable=false, RequestStop=true, ToiletAvailable=false, WheelchairAccessible=false, CarPark=false },
                new Station { StationID=5, StationName="Dolgoch Falls", StationDescription="Another station in delightful surroundings, and a popular place for passengers to stop off awhile to visit the Falls. There is easy access from the station to the Falls area where up to two hours can be spent in exploration. Why not break your journey here, train service permitting? There is a pay and display car park nearby, adjacent to the B4405 road, and there are picnic tables in the station area. Environmentally friendly toilet facilities are provided. Passengers should note that the path to the station is not suitable for the elderly or disabled.", MainlineStationNear=false, RefreshmentsAvailable=false, RequestStop=false, ToiletAvailable=true, WheelchairAccessible=false, CarPark=true },
                new Station { StationID=6, StationName="Abergynolwyn", StationDescription="Abergynolwyn is the main inland station and the starting point for many passengers as there is no road access to our terminus at Nant Gwernol. Station facilities include the Quarryman's Caban Tea Room, shop and toilets (including disabled toilet). At the foot of the (steep) station drive, adjacent to the B4405 road, there is a free car park and a new adventure playground and picnic area. Abergynolwyn is the starting point for a forest walk to Nant Gwernol.", MainlineStationNear=true, RefreshmentsAvailable=true, RequestStop=false, ToiletAvailable=true, WheelchairAccessible=true, CarPark=true },
                new Station { StationID=7, StationName="Nant Gwernol", StationDescription="Nant Gwernol, situated in a natural ravine, is the eastern terminus of the railway and has NO ROAD ACCESS. Access to the station is via the forest walks or the footpath from the car park in Abergynolwyn village, both of which are unsuitable for the elderly or disabled. It is the starting point for walks which enable visitors to explore unspoilt woodland and mountain waterfalls and see something of the former slate mining activities in the area, returning to either Nant Gwernol or Abergynolwyn Stations. All walks are well signposted.", MainlineStationNear=false, RefreshmentsAvailable=false, RequestStop=false, ToiletAvailable=false, WheelchairAccessible=false, CarPark=false }                
            };
            stations.ForEach(s => context.Stations.AddOrUpdate(s));

            var trains = new List<Train> {
                new Train { TrainID=1, TrainNumber=1, Name="Talyllyn", Type="0-4-2ST", WorksNumber=42, Year="1864", Maker="Fletcher, Jennings & Co", Description="Originally built in 1864 by Fletcher, Jennings & Co. of Whitehaven as an 0-4-0ST, 'Talyllyn' had a short wheelbase and long rear overhang which led to its rapid conversion to an 0-4-2ST. As the more popular of the line's two original locos 'Talyllyn' was in very poor condition by 1945 when it was laid aside. It was rebuilt in 1957-58 by Gibbons Bros. Ltd. but proved problematic and has undergone considerable modification since then, resulting in a much improved performance. It is currently running in black livery."  },
                new Train { TrainID=2, TrainNumber=2, Name="Dolgoch", Type="0-4-0 back/well tank", WorksNumber=63, Year="1866", Maker="Fletcher, Jennings & Co", Description="Dolgoch was built in 1866 by Fletcher, Jennings & Co., but to a very different design to that of Talyllyn. It is an 0-4-0 tank engine with both a back tank (behind the cab) and a well tank (between the frames). The long wheelbase allows the firebox to sit in front of the rear axle, with Fletcher's Patent inside valve gear driven off the front axle, a particularly inaccessible arrangement. In increasingly decrepit condition Dolgoch continued to operate the service single-handedly until 1952 when Edward Thomas became available and was then the subject of a prolonged overhaul between 1954 and 1963. Dolgoch returned to service in late 1999 after a major overhaul involving firebox repairs and an extensive mechanical overhaul; as part of the overhaul it has been fitted with air braking equipment, the last steam loco to be so fitted. It is temporarily running in Crimson Lake livery for one year only prior to its ten-yearly boiler  examination."  },
                new Train { TrainID=3, TrainNumber=3, Name="Sir Haydn", Type="0-4-2ST", WorksNumber=323, Year="1878", Maker="Hughes' Loco & Tramway Eng. Works Ltd", Description="Built in 1878 by Hughes' Loco & Tramway Eng. Works Ltd of Loughborough this 0-4-2ST (originally 0-4-0ST) worked on the nearby Corris Railway until the closure of that line in 1948. In 1951 it was purchased by the Talyllyn Railway (along with the other surviving Corris loco which became 'Edward Thomas') and was named after the line's late owner, Sir Henry Haydn Jones. The precarious state of the track led to its being little used for the first few years, and firebox problems caused its withdrawal in 1957. It re-entered service in 1968. 'Sir Haydn' is current running in Corris Railway red livery."  },
                new Train { TrainID=4, TrainNumber=4, Name="Edward Thomas", Type="0-4-2ST", WorksNumber=4047, Year="1921", Maker="Kerr Stuart & Co. Ltd", Description="This 0-4-2ST was built in 1921 by Kerr, Stuart & Co. Ltd. for use on the Corris Railway, and was purchased by the Talyllyn in 1951 and named after the TR's former manager. After repairs carried out by the Hunslet Engine Co., the engine entered service on the Talyllyn in 1952 and has proved most successful. From 1958 until 1969 a Giesel ejector was fitted instead of a conventional chimney, the first such installation in the British Isles. Until 2000 the loco was running in the guise of 'Peter Sam', in red livery. It was then repainted into British Railways black, the colour scheme it might have acquired had the Corris line survived a little longer. An extensive overhaul, which has included the fitting of a new boiler, was completed in late May 2004 and the loco returned to public service on Sunday 30th May as 'Edward Thomas', in unlined green livery. The loco is now running in the standard TR livery of deep bronze lined with black borders and yellow lining."  },
                new Train { TrainID=6, TrainNumber=5, Name="Douglas/Duncan", Type="0-4-0WT", WorksNumber=1431, Year="1918", Maker="Andrew Barclay & Co", Description="This 0-4-0WT was built in 1918 by Andrew Barclay & Co. Ltd. for the Airservice Construction Corps. From 1921 until 1945 it worked at the RAF railway at Calshot Spit, Southampton. After a period in store at Calshot it was bought in 1949 by Abelson & Co. (Engineers) Ltd. who presented it to the Talyllyn in 1953. After overhaul and alteration from 2ft to 2ft 3in gauge, it entered service in 1954 and was named 'Douglas' at the donor's request. Although smaller than the other locos it has performed well and was returned to service in 1995, having been fitted with a new boiler, turned out in its old Air Ministry Works & Buildings livery. It is now painted red and running in the guise of Duncan."  },
                new Train { TrainID=7, TrainNumber=6, Name="Tom Rolt", Type="0-4-2T", WorksNumber=0, Year="1991", Maker="Talyllyn Railway", Description="Tom Rolt was built at the Talyllyn's Pendre Works, incorporating components of a little-used 3ft gauge Andrew Barclay 0-4-0WT built in 1949 for Bord na Mona (the Irish turf board). An 0-4-2T, it is the line's newest, largest and most powerful steam locomotive, having entered service in 1991. It is named after the author L.T.C. Rolt who inspired the Talyllyn's preservation and was its General Manager in 1951-52. In August 2000 'Tom Rolt' returned to service after its 10-yearly boiler inspection and overhaul; it currently carries the standard TR deep bronze green livery."  }      
            };
            trains.ForEach(t => context.Trains.AddOrUpdate(t));

            var fareTypes = new List<FareType>
            {
                new FareType { FaretypeID=1, FareTypeDescription="Adult"},
                new FareType { FaretypeID=2, FareTypeDescription="Over 60s"},
                new FareType { FaretypeID=3, FareTypeDescription="Child (5-15)"},
                new FareType { FaretypeID=4, FareTypeDescription="Dog Rover"},
                new FareType { FaretypeID=5, FareTypeDescription="Under 5s"},
                new FareType { FaretypeID=6, FareTypeDescription="1st Class Upgrade"}
            };
            fareTypes.ForEach(ft => context.FareTypes.AddOrUpdate(ft));

            var ticketGroups = new List<TicketGroup>
            {
                new TicketGroup { TicketTypeID=1, ArrivalStationID=7, DepartureStationID=1, Description="A 2 1/2 hour return trip. Valid for all day travel - hop on and off.", Name="Full Line Return & Day Rover"},
                new TicketGroup { TicketTypeID=2, ArrivalStationID=5, DepartureStationID=1, Description="30 minutes each way. Catch any train back", Name="Tywyn to Dolgoch Return"}
            };
            ticketGroups.ForEach(tg => context.TicketTypes.AddOrUpdate(tg));

            var fares = new List<Fare>
            {
                new Fare { FareID=1, FareTypeID=1, TicketTypeID=1, BasicPrice=14.5, GiftAidPrice=15.95},
                new Fare { FareID=2, FareTypeID=2, TicketTypeID=1, BasicPrice=13, GiftAidPrice=14.3},
                new Fare { FareID=3, FareTypeID=3, TicketTypeID=1, BasicPrice=2, GiftAidPrice=2.2},
                new Fare { FareID=4, FareTypeID=1, TicketTypeID=2, BasicPrice=11, GiftAidPrice=12.1},
                new Fare { FareID=5, FareTypeID=2, TicketTypeID=2, BasicPrice=10, GiftAidPrice=11},
                new Fare { FareID=6, FareTypeID=3, TicketTypeID=2, BasicPrice=2, GiftAidPrice=2.2}
            };
            fares.ForEach(f => context.Fares.AddOrUpdate(f));
        }
    }
}
