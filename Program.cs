// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.VisualBasic;
using Hec.Dss;
using PostProcessor;
using Usace.CC.Plugin;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;

string substitutionStringKey = "substitution_string";
        string datasourceNameKey = "datasource_name";
        string event_or_block = "event_or_block";
        string _substitutionString;
        DataSource _dataSource;

        Console.WriteLine("post-processor2!");

        PluginManager pm = await PluginManager.CreateAsync();
        Payload p = pm.Payload;
        Block myblock = new Block();


        DataSource ds1 = pm.getInputDataSource("BlockFile");
        Console.WriteLine(ds1.Paths[0]);
        //get block file to determine the event range for a realization
        byte[] blockFileBytes = await pm.getFile(pm.getInputDataSource("BlockFile"), 0);
        BlockFile bf = new BlockFile(Encoding.UTF8.GetString(blockFileBytes));//verify utf8

        _substitutionString = p.Attributes[substitutionStringKey];
        string datasourcename = p.Attributes[datasourceNameKey];
        string processBy = p.Attributes[event_or_block];
        bool byBlock = true;
        if (processBy == "event"){
            byBlock = false;
        }
        _dataSource = pm.getInputDataSource(datasourcename);
        //get the "event number" from the plugin manager through the environment variable, interpret it as the realization number for the frequency curve
        int eventNumber = pm.EventNumber();
        //download the dss files for the events in the realization
        //safety first?!
        if (!Directory.Exists("/data"))
        {
            Directory.CreateDirectory("/data");
        }
        else
        {
            string[] filenames = Directory.GetFiles("/data");
            foreach (string fn in filenames)
            {
                File.Delete(fn);
            }
        }
        //verify substitution string is not null
        if (_substitutionString == null)
        {
            //pitch a fit!
            return;
        }
        int blockCount = 0;
        int eventCount = 0;
        long eventStartIndex = 1;
        foreach (Block b in bf.Blocks)
        {
            if (b.RealizationIndex == eventNumber)//using event number to understand which realization to use
            {
                blockCount++;
                eventCount += b.BlockEventCount;
                if (b.BlockIndex == 1){
                    eventStartIndex = b.BlockEventStart;
                }
            }
            
        }
        //need one watershed result per action
        Dictionary<Usace.CC.Plugin.Action,WatershedResult> results = new Dictionary<Usace.CC.Plugin.Action, WatershedResult>();
        Dictionary<Usace.CC.Plugin.Action, DSSAction> actions = new Dictionary<Usace.CC.Plugin.Action, DSSAction>();
        foreach(Usace.CC.Plugin.Action a in p.Actions){
            if(byBlock){
                results[a] = new WatershedResult(_dataSource.DataPaths, blockCount, 0, byBlock);
            }else{
                results[a] = new WatershedResult(_dataSource.DataPaths, eventCount, (int)eventStartIndex, byBlock);
            }
            
            switch (a.Name)
            {
                case "dss_peak":
                actions[a] = new DssPeaksAction(a);
                    break;
                case "dss_peak_volume_duration":
                    actions[a] = new DssPeakVolumeDurationAction(a);
                    break;
                case "dss_peak_duration":
                    actions[a] = new DssPeakDurationAction(a);
                    break;
                default:
                    break;
            }
        }
        string dssFilePathPattern = _dataSource.Paths[0];
        string localPath = "/data/file.dss";
        int progress = 0;
        foreach (Block b in bf.Blocks)
        {
            if (b.RealizationIndex == eventNumber)
            {
                    progress = (int)(100.0 * b.BlockIndex / blockCount);
                    pm.ReportProgress(new Status(Status.StatusLevel.COMPUTING, progress));
                    pm.LogMessage(new Message("Processing Block " + b.BlockIndex + " of " + blockCount));
                for (long i = b.BlockEventStart; i <= b.BlockEventEnd; i++)
                {//if a block has no events - this kinda breaks down alittle bit.
                 //download each event level dss file.
                    string dssFilePath = Strings.Replace(dssFilePathPattern, _substitutionString, i.ToString(), 1, -1, CompareMethod.Binary);
                    pm.LogMessage(new Message("Processing " + dssFilePath));
                    _dataSource.Paths[0] = dssFilePath;
                    Stream dssStream = await pm.FileReader(_dataSource, 0);
                    using (var fs = new FileStream("/data/file.dss", FileMode.Create))
                    {
                        dssStream.CopyTo(fs);
                    }
                        DssReader reader = new DssReader(localPath, 0);
                        foreach (String recordName in _dataSource.DataPaths)
                        {
                            DssPath dsspath = new DssPath(recordName);
                            try
                            {
                                RecordType rt = reader.GetRecordType(dsspath);
                                if (rt == RecordType.RegularTimeSeries)
                                {
                                    TimeSeries ts = reader.GetTimeSeries(dsspath);
                                    foreach (Usace.CC.Plugin.Action a in p.Actions)
                                    {
                                        results[a].UpdateLocation(recordName,b.BlockIndex,(int)i, actions[a].Compute(ts));//-(int)eventStartIndex
                                    }
                                }
                            }catch(Exception ex){
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(recordName + " not found.");
                                return;
                            }
                        }
                }
            }
        }
        //write out for the output locations
        foreach (Usace.CC.Plugin.Action a in p.Actions){
            DSSAction da = actions[a];
            DataSource ds = pm.getOutputDataSource(da.OutputDataSourceName());
            byte[] data = results[a].Write();
            MemoryStream ms = new MemoryStream(data);
            bool success = await pm.FileWriter(ms, ds, 0);
        }
        pm.ReportProgress(new Status(Status.StatusLevel.SUCCEEDED, 100));
