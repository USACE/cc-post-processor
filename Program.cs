// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Windows.Markup;
using Hec.Dss;
using Usace.CC.Plugin;

Console.WriteLine("post-processor!");

PluginManager pm = await PluginManager.CreateAsync();
Payload p = pm.Payload;
String runs_dir = p.Attributes["runs_dir"];
String event_path = p.Attributes["event_path"];
String tmp_start = p.Attributes["start_idx"];
string tmp_end = p.Attributes["end_idx"];
int start = int.Parse(tmp_start);
int end = int.Parse(tmp_end);
String[] records = new String[]{
    "//Alderson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Alderson_to_Hilldale/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Alisonia_to_Claytor Lk/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Allisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Ashford_to_Tornado/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//BLN Inflow Indian Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//BLN Inflow Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bane/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Belva/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Belva_to_Gauley Brg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Birch River Reach/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//BirchR_to_Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bluestone Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bluestone Lake HW/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bluestone Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bluestone Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Bluestone Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//BluestoneOF_to_Greenbrier/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Buck Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Buck Dam_to_Alisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Buckeye/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Buckeye_to_Caldwell/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cabin Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cabin Ck_to_Mamet LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cabin Cr - Kan Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cabin Creek/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Caldwell/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Caldwell_to_Alderson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Camden On The Gauley/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Camden to Cranberry R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Charleston Lock 6/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Charleston SSRRB/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//CharlestonElk/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Charleston_to_Elk R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Charlestone LK6_to_Coal R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Clay/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//ClayL_TO_RppOutflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Clay_TO_QueenS/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Claytor Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Claytor Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Claytor Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Clytor Lk_to_RRP at New R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Coal R_to_Pocatalico R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Coal River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Craigsville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Craigsville_to_Summersville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cranberry R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cranberry R_to_Craigsville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cranberry River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Cranberry River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//East R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//East R - New River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Elk R_to_Charleston LK6/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Elk- Birch R Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Falls Mills/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Falls Mills_to_Spanishburg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Frametown_to_Clay/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Galax/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Galax_to_BuckDam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Gauley Brg_to_Kanwaha Falls/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Gauley River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Glen Lynn/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Glen Lynn_to_East R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Graysonton_to_RRP at New R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Graysontown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Greenbrier R_to_Hinton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Greenbrier River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Guardian/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Herold/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Hilldale/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Hilldale_to_Greenbrier Mouth/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Hinton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Hinton_to_Piney Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Holly River/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Indian Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Jefferson/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Jefferson_to_Galax/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Junction 93/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Junction 95/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Kanawha Falls/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Kanawha Falls_to_London LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Left Fk Holly R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Little River Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Lockwood/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//London LD_to_Paint Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//London Lock and Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Marmet LD_to_Charleston/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Marmet Lock and Dam/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Meadow R_to_Peters Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Meadow River conf/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Mt Lookout_to_Meadow R Mouth/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Mt. Lookout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Nallen/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Nallen_to_Mt Lookout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Narrows/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Null BLN INflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Null BLN Inflow2/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Paint Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Paint Ck_to_Cabin Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Paint Cr - Kan Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Paint Creek/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Peters Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Peters Ck_to_Belva/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Piney Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Piney Ck_to_Thurmond/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Piney Cr - New Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Pipestem_to_Bluestone R Mout/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Poca R_to_Winfiled LD/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Pocatalico R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Point Pleasant/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Potalico R - Kan R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Queen Shoals/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Queen Shoals_to_Elk R at Kan/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//RPPDam Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//RRP at New R_to_Radford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Radford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Radford Source/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Radford_to_Walker Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Raleigh/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Red Sulphur Springs/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Reed Island Ck_to_Alisonia/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Reeds Island Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Replete/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Right Fk Holly R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//RppLake Reach/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sissonville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Spanishburg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Spanishburg_to_Pipestem/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Summersville Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Summersville Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Summersville Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Summersville Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//SummersvilleOF-MeadowJun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sutton Inflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sutton Outflow/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sutton Res/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sutton Rted/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Sutton_to_Frametown/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Thirteenmile_to_Pt Pleasant/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Thirteenmilel Cr/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Thurmond/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Thurmond_to_Gauley Brg/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Tornado/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Tornado_to_Coal R at Kan R/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Walker Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Walker Ck_to_Wolf Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Walker Creek Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Webster Springs/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//WebsterSpr_to_Palmer/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Whitesville/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Whitesville to Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Whitesville to Clear Fk Jun/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Whitesville_to_Ashford/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Willowton/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Winfield/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Winfield LD_to_Thirteenmile/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Wolf Ck/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Wolf Ck_to_Glen Lynn/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
"//Wolf Creek Confluence/FLOW/14Jan1996 - 07Feb1996/1Hour/RUN:Jan 1996 - Calibration/",
};
DataSource dataSource = p.Inputs[0];
string header = "event_number";
foreach(String recordName in records){
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
    dataSource.Paths[0] = runs_dir + "/" + i + "/" + event_path;
    byte[] ret = await pm.getFile(dataSource,0);
    //write bytes to local path.
    System.IO.File.WriteAllBytes("/data/file.dss",ret);
    String path = "/data/file.dss";
    DssReader reader = new DssReader(path,0);
    int idx = 0;
    foreach(String recordName in records){
        DssPath dsspath = new DssPath(recordName);
        RecordType rt = reader.GetRecordType(dsspath);
        if(rt==RecordType.RegularTimeSeries){  
            Double[] values = reader.GetTimeSeries(dsspath).Values;
            line += ","+values.Max();
            idx ++;
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
