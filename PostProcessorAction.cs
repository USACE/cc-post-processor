using Hec.Dss;

namespace PostProcessor
{
    public interface DSSAction{
        public double Compute(TimeSeries ts);
        public string OutputDataSourceName();
    }
}