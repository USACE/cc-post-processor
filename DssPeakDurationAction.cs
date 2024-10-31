using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Hec.Dss;
using Microsoft.VisualBasic;
using Usace.CC.Plugin;

namespace PostProcessor
{
    public class DssPeakDurationAction : DSSAction
    {
        private static string timeSetpsKey = "timesteps";
        private int _timesteps = 0;
        private static string outputDataSourceNameKey = "output_datasource_name";
        private string _outputDataSourceString;

        public DssPeakDurationAction(Usace.CC.Plugin.Action a)
        {
            _timesteps = Int32.Parse(a.Parameters[timeSetpsKey]);
            _outputDataSourceString = a.Parameters[outputDataSourceNameKey];
        }
        public double Compute(TimeSeries ts)
        {
            double[] values = ts.Values;
            double maxval = 0.0;
            double runningVal = 0.0;
            for (int timestep = 0; timestep < values.Length; timestep++)
            {
                runningVal += values[timestep];
                if (timestep < _timesteps)
                {
                    maxval = runningVal;
                }
                else
                {
                    runningVal -= values[timestep - _timesteps];
                    if (runningVal > maxval)
                    {
                        maxval = runningVal;
                    }
                }
            }
            return maxval/(double)_timesteps;
        }
        public string OutputDataSourceName(){
            return _outputDataSourceString;
        }
    }
}