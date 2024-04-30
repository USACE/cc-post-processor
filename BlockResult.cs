
namespace PostProcessor
{
    public class BlockResult{
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
    }
}