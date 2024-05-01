using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Hec.Dss;
using Microsoft.VisualBasic;
using Usace.CC.Plugin;

namespace PostProcessor
{
    public class DssPeaksAction{
        private int _realization;
        private BlockFile _blockFile;
        private DataSource _dataSource;
        private static string substitutionStringKey = "substitution_string";
        private static string datasourceNameKey = "datasource_name";
        private static string outputDataSourceNameKey = "output_datasource_name";
        private string _outputDataSourceString;
        private string _substitutionString;
        public DssPeaksAction(Usace.CC.Plugin.Action a, PluginManager pm, BlockFile blockfile){
            //get the event number specified by the environment variable to interpret as the realization number
            _realization = pm.EventNumber();
            //get the substitution string from the action
            _substitutionString = a.Parameters[substitutionStringKey];
            string datasourcename = a.Parameters[datasourceNameKey];
            _dataSource = pm.getInputDataSource(datasourcename);
            _outputDataSourceString = a.Parameters[outputDataSourceNameKey];
            _blockFile = blockfile;
        }
        public async Task<bool> Compute(PluginManager pm){
            //safety first?!
            if(!System.IO.Directory.Exists("/data")){
                System.IO.Directory.CreateDirectory("/data");
            }else{
                string[] filenames = System.IO.Directory.GetFiles("/data");
                foreach(string fn in filenames){
                        System.IO.File.Delete(fn);
                }
            }
            //verify substitution string is not null
            if (_substitutionString == null){
                //pitch a fit!
                return false;
            }
            int blockCount = 0;
            foreach(Block b in _blockFile.Blocks){
                if (b.RealizationIndex==_realization){
                    blockCount ++;
                }
            }
            WatershedResult results = new WatershedResult(_dataSource.DataPaths,blockCount);

            string dssFilePathPattern = _dataSource.Paths[0];
            string localPath = "/data/file.dss";
            foreach(Block b in _blockFile.Blocks){
                if (b.RealizationIndex == _realization){
                    for(Int64 i = b.BlockEventStart; i <= b.BlockEventEnd; i++ ){//if a block has no events - this kinda breaks down alittle bit.
                        //download each event level dss file.
                        string dssFilePath = Strings.Replace(dssFilePathPattern,_substitutionString,i.ToString(),1,-1,CompareMethod.Binary);
                        _dataSource.Paths[0] = dssFilePath;
                        System.IO.Stream dssStream = await pm.FileReader(_dataSource,0);
                        using(var fs = new FileStream("/data/file.dss",FileMode.Create)){
                            dssStream.CopyTo(fs);//@TODO:verify this
                        }
                            DssReader reader = new DssReader(localPath,0);
                            int idx = 0;
                            foreach(String recordName in _dataSource.DataPaths){
                                DssPath dsspath = new DssPath(recordName);
                                try{
                                    RecordType rt = reader.GetRecordType(dsspath);
                                    if(rt==RecordType.RegularTimeSeries){  
                                        double[] values = reader.GetTimeSeries(dsspath).Values;
                                        results.UpdateLocation(recordName, b.BlockIndex,(int)i,values.Max());
                                        idx ++;
                                    }            
                                }catch(Exception ex){
                                    Console.WriteLine(recordName + " not found.");
                                    return false;
                                }

                            }
                    }
                }
            }
            results.Sort();
            //write out results for each location
            byte[] bytes = results.Write(_realization);
            MemoryStream ms = new MemoryStream(bytes);
            DataSource outputDest = pm.getOutputDataSource(_outputDataSourceString);
            bool success = await pm.FileWriter(ms,outputDest,0);
            
            return success;
        }
    }
}