
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Hec.Dss;
using Microsoft.VisualBasic;
using Usace.CC.Plugin;

namespace PostProcessor
{
    public class DssPeaksAction: DSSAction{
        private static string outputDataSourceNameKey = "output_datasource_name";
        private string _outputDataSourceString;
        public DssPeaksAction(Usace.CC.Plugin.Action a){
            _outputDataSourceString = a.Parameters[outputDataSourceNameKey];
        }
        public double Compute(TimeSeries ts){

            double[] values = ts.Values;
            return values.Max();
        }
        public string OutputDataSourceName(){
            return _outputDataSourceString;
        }
    }
}