using System.Reflection.Metadata.Ecma335;

namespace PostProcessor
{
    public class LocationResult{
        public string Location;
        public Result[] Results;
        private bool byBlock;
        public LocationResult(string locationName, int count, bool byBlock){
            Location = locationName;
            this.byBlock = byBlock;
            if (byBlock){
                Results = new BlockResult[count];
                for( int i = 0; i<count; i++){
                        Results[i] = new BlockResult(i+1);
                }
            }else{
                Results = new EventResult[count];
                for( int i = 0; i<count; i++){
                        Results[i] = new EventResult(i+1);
                }
             }
        }
        public void Update(int blockId, int eventId, double value){
            if (byBlock){
                Results[blockId-1].Update(blockId, eventId, value);
            }else{
                Results[eventId].Update(blockId, eventId, value);
            }
            
        }
        public void Sort(){
            Array.Sort(Results);
        }
    }
}