using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostProcessor
{
    public class WatershedResult{
        public LocationResult[] Locations;
        public WatershedResult(string[] locations, int blockCount){
            Locations = new LocationResult[locations.Length];
            int idx = 0;
            foreach(string l in locations){
                Locations[idx] = new LocationResult(l,blockCount);
                idx++;
            }
        }
        public void UpdateLocation(string locationName, int blockId, int eventId, double value){
            int idx = 0;
            foreach(LocationResult l in Locations){
                if (l.Location.Equals(locationName)){
                    l.UpdateBlock(blockId,eventId,value);
                    Locations[idx] = l;//unnecessary?
                    return;
                }
                idx++;
            }
        }
        public void Sort(){
            for( int i=0; i<Locations.Length;i++){
                Locations[i].Sort();
            }
        }

        internal byte[] Write(int realization)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Frequency");
            foreach(LocationResult l in Locations){
                sb.Append("," + l.Location +"_BlockID," + l.Location + "_EventID," + l.Location + "_Value");
            }
            sb.Append("\n");
            int count = Locations[0].BlockResults.Length;
            for(int i = 0; i < count; i ++){
                sb.Append((float)i/(float)count);
                foreach(LocationResult l in Locations){
                    sb.Append("," + l.BlockResults[i].BlockNumber +"," + l.BlockResults[i].EventNumber + "," + l.BlockResults[i].Value + "");
                }
                sb.Append("\n");
            }
            return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}