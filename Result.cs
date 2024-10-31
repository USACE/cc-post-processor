using Hec.Dss;

namespace PostProcessor
{
    public interface Result: IComparable<Result>{
        public void Update(int blockNumber, int eventNumber, double value);
        double Value {
            get;
        }
        int Block {
            get;
        }
        int Event {
            get;
        }

    }
}