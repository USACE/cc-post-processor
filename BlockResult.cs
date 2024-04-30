
namespace PostProcessor
{
    public class BlockResult : IComparable<BlockResult>{
        public int BlockNumber = -1;
        public int EventNumber = -1;
        public double Value = float.MinValue;
        public BlockResult(int blockNumber){
            BlockNumber = blockNumber;
        }
        internal void UpdateValue(int blockNumber, int eventNumber, double value)
        {
            if (Value<value){
                Value = value;
                EventNumber = eventNumber;
                BlockNumber = blockNumber;
            }
        }
        public int CompareTo(BlockResult? other)
        {
            if(this.Value == other.Value){
                return 0;
            }else if(this.Value> other.Value){
                return 1;
            }else if(this.Value<other.Value){
                return -1;
            }else{
                return -1;
            }
        }
    }
}