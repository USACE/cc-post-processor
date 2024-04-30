using System.ComponentModel.DataAnnotations;

namespace PostProcessor
{
    public class WatershedResult{
        public LocationResult[] Locations;
        public WatershedResult(string[] locations, int blockCount){
            Locations = new LocationResult[locations.Length];
            int idx = 0;
            foreach(string l in locations){
                Locations[0] = new LocationResult(l,blockCount);
                idx++;
            }
        }
        public void UpdateLocation(string locationName, int blockId, int eventId, double value){
            int idx = 0;
            foreach(LocationResult l in Locations){
                if (l.Equals(locationName)){
                    l.UpdateBlock(blockId,eventId,value);
                    Locations[idx] = l;//unnecessary?
                    return;
                }
                idx++;
            }
        }
    }
}