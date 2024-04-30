﻿// See https://aka.ms/new-console-template for more information
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

foreach(Usace.CC.Plugin.Action a in p.Actions){
    switch(a.Name){
        case "dss_peak":
            DssPeaksAction dpa = new DssPeaksAction(a,pm,bf);
            if(! await dpa.Compute(pm)){
                pm.ReportProgress(new Status(Status.StatusLevel.FAILED,50));

                return;
            }
            break;
        default:
            break;
    }
}
pm.ReportProgress(new Status(Status.StatusLevel.SUCCEEDED,100));

