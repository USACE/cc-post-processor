using System.Collections;
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
        private static double StandardNormalInverseCDF(double p)
        {
            //if (p <= 0) return Double.NegativeInfinity;
            //if (p >= 1) return Double.PositiveInfinity;
            //if (StandardDeviation == 0) return Mean;
            //return invCDFNewton(p, Mean, 1e-10,100);
            int i;
            double x;
            double q;
            double c0 = 2.515517;
            double c1 = .802853;
            double c2 = .010328;
            double d1 = 1.432788;
            double d2 = .189269;
            double d3 = .001308;
            q = p;
            if (q == .5) { return 0.0; }
            if (q <= 0) { q = .000000000000001; }
            if (q >= 1) { q = .999999999999999; }
            if (q < .5) { i = -1; }
            else
            {
                i = 1;
                q = 1 - q;
            }

            double t = Math.Sqrt(Math.Log(1 / (q * q)));
            double tsquared = t * t;
            double tcubed = tsquared * t;
            x = t - (c0 + c1 * t + c2 * (tsquared)) / (1 + d1 * t + d2 * tsquared + d3 * tcubed);
            x = i * x;
            return (x * 1.0);

        }
        internal byte[] Write()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Frequency, NonExceedence, Plotting Position, Z Score");
            foreach(LocationResult l in Locations){
                sb.Append("," + l.Location +"_BlockID," + l.Location + "_EventID," + l.Location + "_Value");
            }
            sb.Append("\n");
            int count = Locations[0].BlockResults.Length;
            for(int i = 0; i < count; i ++){
                float frequency =(float)i/(float)count;
                sb.Append(frequency);
                sb.Append(",");
                sb.Append(1.0-frequency);
                sb.Append(",");
                float plottingPosition = (float)(count-i)/(float)(count+1);
                sb.Append(plottingPosition);
                sb.Append(",");
                sb.Append(StandardNormalInverseCDF(plottingPosition));
                sb.Append(",");
                foreach(LocationResult l in Locations){
                    sb.Append("," + l.BlockResults[i].BlockNumber +"," + l.BlockResults[i].EventNumber + "," + l.BlockResults[i].Value + "");
                }
                sb.Append("\n");
            }
            return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
        }
        internal byte[] WriteImportantEvents()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList eventSet = new System.Collections.ArrayList();
            int blockcount = Locations[0].BlockResults.Length;
            for(int i = 0; i < blockcount; i ++){
                foreach(LocationResult l in Locations){
                    if (!eventSet.Contains(l.BlockResults[i].EventNumber)){
                        eventSet.Add(l.BlockResults[i].EventNumber);
                    }
                }
            }
            foreach(int e in eventSet){
                sb.Append(",");
                sb.Append(e);
            }
            sb.Append("\n");
            return System.Text.Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}