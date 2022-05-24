using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class ExportTrialData
{
    public static DataCollection trialDatas = new DataCollection();
    public static List<MovementData> movements = new List<MovementData>();
    public static void WriteTrials(string file, DataCollection records)
    {

        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);

        for(int i=0; i< records.Count; i++)
        {
            tw.WriteLine("Trial Number, Axis, Number of Instructions, Time Taken, Trial Success, Game Directions");
            tw.WriteLine(records[i].trialNumber+","+ records[i].axis + "," + records[i].numberOfDirections + ","+ records[i].timeTaken + "," + records[i].success.ToString() + ",Direction number, Distance to Target, Direction Given, Angle from Reference, Time into trial, Time Since Start, DirResponse, MouseXRes, MouseYRes");
            //tw.Write(",,,,,Direction number, Distance to Target, Direction Given, Angle from Reference, Elapsed Time, DirResponse, MouseXRes, MouseYRes");
            for (int j=0; j<records[i].gameDirections.Count; j++)
            {
                tw.WriteLine(",,,,," + records[i].gameDirections[j].id + "," + records[i].gameDirections[j].distanceToTarget + "," + records[i].gameDirections[j].direction + "," + records[i].gameDirections[j].angleToTarget + "," + records[i].gameDirections[j].timeElapsed +","+ records[i].gameDirections[j].timeSinceStart + "," + records[i].gameDirections[j].response.firstKeyStroke + "," + records[i].gameDirections[j].response.firstMouseX + "," + records[i].gameDirections[j].response.firstMouseY);
                //if (records[i].gameDirections[j].response != null)
                //{
                //    tw.WriteLine(",,,,,,,,,," + records[i].gameDirections[j].response.firstKeyStroke + "," + records[i].gameDirections[j].response.firstMouseX + "," + records[i].gameDirections[j].response.firstMouseY);
                //}
                
            }
        }

        tw.Close();
    }
    public static void ExportData(DataCollection trialDatas)
    {
        //Export the data to csv
        if (!Directory.Exists(Application.streamingAssetsPath + "/Export"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Export");
        }
        string path = Application.streamingAssetsPath + "\\Export\\";
        string exportFile = (DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")).ToString();
        string exportPath = path + exportFile + "trial-records.csv";
        ExportTrialData.WriteTrials(exportPath, trialDatas);

        exportPath = path + exportFile + "instruction-records.csv";
        ExportTrialData.WriteDirections(exportPath, trialDatas);

        ////Export data to xml
        //var serializer = new XmlSerializer(typeof(DataCollection));
        //var stream = new FileStream(path + exportFile + "-records.xml", FileMode.Create);
        //serializer.Serialize(stream, trialDatas);
    }
    public static void ExportMovement(List<MovementData> movement)
    {
        //Export the data to csv
        if (!Directory.Exists(Application.streamingAssetsPath + "/Export"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Export");
        }
        string path = Application.streamingAssetsPath + "\\Export\\";
        string exportFile = (DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")).ToString();
        string exportPath = path + exportFile + "-movement.csv";
        ExportTrialData.WriteMovement(exportPath, movement);

        ////Export data to xml
        //var serializer = new XmlSerializer(typeof(DataCollection));
        //var stream = new FileStream(path + exportFile + "-movement.xml", FileMode.Create);
        //serializer.Serialize(stream, tl);
    }
    public static void WriteMovement(string file, List<MovementData> movement)
    {

        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);
            tw.WriteLine("PosX, PosY, PosZ, RotX, RotY, Current Trial, Time Since Game Start, Closer/Further");
        for (int i = 0; i < movement.Count; i++)
        {
            tw.WriteLine(movement[i].pos.x + "," + movement[i].pos.y + "," + movement[i].pos.z + "," + movement[i].rotX + "," + movement[i].rotY + "," + movement[i].currentTrial + "," + movement[i].timeSinceStart + "," + movement[i].closerOrFurther);
        }

        tw.Close();
    }

    public static void WriteDirections(string file, DataCollection records)
    {
        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);

        for (int i = 0; i < records.Count; i++)
        {
            tw.WriteLine("Trial no., Axis, Direction number, Distance to Target, Direction Given, Angle from Reference, Time into trial, Time Since Start, DirResponse, MouseXRes, MouseYRes");
            for (int j = 0; j < records[i].gameDirections.Count; j++)
            {
                tw.WriteLine(records[i].trialNumber + "," + records[i].axis + "," + records[i].gameDirections[j].id + "," + records[i].gameDirections[j].distanceToTarget + "," + records[i].gameDirections[j].direction + "," + records[i].gameDirections[j].angleToTarget + "," + records[i].gameDirections[j].timeElapsed + "," + records[i].gameDirections[j].timeSinceStart + "," + records[i].gameDirections[j].response.firstKeyStroke + "," + records[i].gameDirections[j].response.firstMouseX + "," + records[i].gameDirections[j].response.firstMouseY);
            }
        }

        tw.Close();
    }
}
