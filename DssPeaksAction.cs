using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Hec.Dss;
using Usace.CC.Plugin;

namespace PostProcessor
{
    public class DssPeaksAction{
        private string _dssFileName;
        private string _rootDirectory;
        private int _startIndex;
        private string[] _dssPaths;
        private static string substitutionString = "eventnumber";
        public DssPeaksAction(){

        }
        public bool Compute(){
            return true;
            /*
            DataSource dataSource = p.Inputs[0];
string header = "event_number";
foreach(String recordName in dataSource.DataPaths){
    header = header + "," + recordName;
}
header = header + "\n";
if(!System.IO.Directory.Exists("/data")){
    System.IO.Directory.CreateDirectory("/data");
}else{
   string[] filenames = System.IO.Directory.GetFiles("/data");
   foreach(string fn in filenames){
        System.IO.File.Delete(fn);
   }
}
string outputPath = "/data/output.csv";
FileStream output = System.IO.File.Create(outputPath);
output.Write(Encoding.ASCII.GetBytes(header));
string line = "";
for (int i = start; i <= end; i++){
    line = i.ToString();
    Console.WriteLine("Processing event " + line);
    dataSource.Paths[0] = runs_dir + "/" + i + "/" + event_path;
    byte[] ret = await pm.getFile(dataSource,0);
    //write bytes to local path.
    System.IO.File.WriteAllBytes("/data/file.dss",ret);
    String path = "/data/file.dss";
    DssReader reader = new DssReader(path,0);
    int idx = 0;
    foreach(String recordName in records){
        DssPath dsspath = new DssPath(recordName);
        try{
            RecordType rt = reader.GetRecordType(dsspath);
            if(rt==RecordType.RegularTimeSeries){  
                Double[] values = reader.GetTimeSeries(dsspath).Values;
                line += ","+values.Max();
                idx ++;
            }            
        }catch(Exception ex){
            line += ","+Double.NaN;
            Console.WriteLine(recordName + " not found.");
        }

    }
    line += "\n";
    output.Write(Encoding.ASCII.GetBytes(line));
    reader.Dispose();
}
output.Close();
output.Dispose();
//upload output to s3.
DataSource outputDest = p.Outputs[0];
bool success = await pm.PutFile(outputPath,outputDest,0);
*/
        }
    }
}