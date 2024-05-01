using System.Reflection.Metadata.Ecma335;

namespace PostProcessor
{
    public class LocationResult{
        public string Location;
        public BlockResult[] BlockResults;
        public LocationResult(string locationName, int blockCount){
            Location = locationName;
            BlockResults = new BlockResult[blockCount];//-1?
            for( int i = 0; i<blockCount; i++){
                BlockResults[i] = new BlockResult(i+1);
            }
        }
        public void UpdateBlock(int blockId, int eventId, double value){
            BlockResults[blockId-1].UpdateValue(blockId, eventId, value);
        }
        public void Sort(){
            Array.Sort(BlockResults);
        }
    }
}