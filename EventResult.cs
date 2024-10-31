
namespace PostProcessor
{
    public class EventResult : Result, IComparable<Result>{
        private int _blockNumber = -1;
        private int _eventNumber = -1;
        private double _value = float.MinValue;
        public int Block{ get{return _blockNumber;} private set{_blockNumber = value;}}
        public int Event{ get{return _eventNumber;} private set{_eventNumber = value;}}
        public double Value{ get{return _value;} private set{_value = value;}}
        public EventResult(int eventNumber){
            Event = eventNumber;
        }
        public void Update(int blockNumber, int eventNumber, double inValue)
        {
                this.Value = inValue;
                this.Event = eventNumber;
                this.Block = blockNumber;
        }
        public int CompareTo(Result? other)
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