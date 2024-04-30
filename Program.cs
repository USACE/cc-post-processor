// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Text;
using Hec.Dss;
using PostProcessor;
using Usace.CC.Plugin;

Console.WriteLine("post-processor!");

PluginManager pm = await PluginManager.CreateAsync();
Payload p = pm.Payload;

//get block file to determine the event range for a realization
byte[] blockFileBytes = await pm.getFile(pm.getInputDataSource("BlockFile"),0);
BlockFile bf = new BlockFile(System.Text.Encoding.UTF8.GetString(blockFileBytes));//verify utf8

//get the "event number" from the plugin manager through the environment variable, interpret it as the realization number for the frequency curve
int eventNumber = pm.EventNumber();
//download the dss files for the events in the realization


Usace.CC.Plugin.Action[] a = p.Actions;
String runs_dir = p.Attributes["runs_dir"];
String event_path = p.Attributes["event_path"];
String tmp_start = p.Attributes["start_idx"];
string tmp_end = p.Attributes["end_idx"];
int start = int.Parse(tmp_start);
int end = int.Parse(tmp_end);
//
String[] records = new String[]{
    "//Alderson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Alderson_to_Hilldale/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Alisonia_to_Claytor Lk/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Allisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Ashford_to_Tornado/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//BLN Inflow Indian Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//BLN Inflow Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bane/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Belva/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Belva_to_Gauley Brg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Birch River Reach/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//BirchR_to_Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bluestone Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bluestone Lake HW/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bluestone Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bluestone Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Bluestone Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//BluestoneOF_to_Greenbrier/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Buck Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Buck Dam_to_Alisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Buckeye/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Buckeye_to_Caldwell/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cabin Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cabin Ck_to_Mamet LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cabin Cr - Kan Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cabin Creek/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Caldwell/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Caldwell_to_Alderson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Camden On The Gauley/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Camden to Cranberry R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Charleston Lock 6/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Charleston SSRRB/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//CharlestonElk/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Charleston_to_Elk R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Charlestone LK6_to_Coal R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Clay/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//ClayL_TO_RppOutflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Clay_TO_QueenS/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Claytor Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Claytor Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Claytor Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Clytor Lk_to_RRP at New R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Coal R_to_Pocatalico R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Coal River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Craigsville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Craigsville_to_Summersville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cranberry R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cranberry R_to_Craigsville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cranberry River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Cranberry River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//East R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//East R - New River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Elk R_to_Charleston LK6/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Elk- Birch R Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Falls Mills/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Falls Mills_to_Spanishburg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Frametown_to_Clay/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Galax/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Galax_to_BuckDam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Gauley Brg_to_Kanwaha Falls/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Gauley River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Glen Lynn/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Glen Lynn_to_East R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Graysonton_to_RRP at New R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Graysontown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Greenbrier R_to_Hinton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Greenbrier River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Guardian/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Herold/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Hilldale/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Hilldale_to_Greenbrier Mouth/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Hinton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Hinton_to_Piney Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Holly River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Indian Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Jefferson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Jefferson_to_Galax/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Junction 93/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Junction 95/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Kanawha Falls/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Kanawha Falls_to_London LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Left Fk Holly R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Little River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Lockwood/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//London LD_to_Paint Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//London Lock and Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Marmet LD_to_Charleston/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Marmet Lock and Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Meadow R_to_Peters Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Meadow River conf/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Mt Lookout_to_Meadow R Mouth/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Mt. Lookout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Nallen/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Nallen_to_Mt Lookout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Narrows/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Null BLN INflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Null BLN Inflow2/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Paint Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Paint Ck_to_Cabin Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Paint Cr - Kan Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Paint Creek/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Peters Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Peters Ck_to_Belva/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Piney Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Piney Ck_to_Thurmond/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Piney Cr - New Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Pipestem_to_Bluestone R Mout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Poca R_to_Winfiled LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Pocatalico R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Point Pleasant/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Potalico R - Kan R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Queen Shoals/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Queen Shoals_to_Elk R at Kan/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//RPPDam Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//RRP at New R_to_Radford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Radford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Radford Source/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Radford_to_Walker Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Raleigh/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Red Sulphur Springs/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Reed Island Ck_to_Alisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Reeds Island Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Replete/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Right Fk Holly R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//RppLake Reach/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sissonville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Spanishburg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Spanishburg_to_Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Summersville Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Summersville Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Summersville Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Summersville Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//SummersvilleOF-MeadowJun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sutton Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sutton Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sutton Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sutton Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Sutton_to_Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Thirteenmile_to_Pt Pleasant/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Thirteenmilel Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Thurmond/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Thurmond_to_Gauley Brg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Tornado/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Tornado_to_Coal R at Kan R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Walker Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Walker Ck_to_Wolf Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Walker Creek Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Webster Springs/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//WebsterSpr_to_Palmer/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Whitesville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Whitesville to Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Whitesville to Clear Fk Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Whitesville_to_Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Willowton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Winfield/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Winfield LD_to_Thirteenmile/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Wolf Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Wolf Ck_to_Glen Lynn/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
"//Wolf Creek Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:POR/",
};
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
if (success){
    pm.ReportProgress(new Status(Status.StatusLevel.SUCCEEDED,100));
}else{
    pm.ReportProgress(new Status(Status.StatusLevel.FAILED,99));
}
