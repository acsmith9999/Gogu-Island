using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public static class ExportTrialData
{
    public static DataCollection trialDatas = new DataCollection();
    public static List<MovementData> movements = new List<MovementData>();
    public static List<Gazes> gazeDatas = new List<Gazes>();

    /// <summary>
    /// Write all information about all trials
    /// </summary>
    /// <param name="file"></param>
    /// <param name="records"></param>
    public static void WriteTrials(string file, DataCollection records)
    {
        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);
        tw.WriteLine("Sequence, Trial Number, Axis, Number of Instructions, Time Taken, Trial Success, Game Directions");
        for(int i=0; i< records.Count; i++)
        {
            tw.WriteLine(records[i].sequenceNumber + ","
                + records[i].trialNumber+","
                + records[i].axis + "," 
                + records[i].numberOfDirections + ","
                + records[i].timeTaken + "," 
                + records[i].success.ToString() 
                + ",Direction number, Source, Distance to Target, Direction Given, DirectionRef, Angle from Reference, Time into trial, Time Since Start, DirResponse, KeyOnset, MouseXRes, MouseYRes, MouseOnset");
            //tw.Write(",,,,,Direction number, Distance to Target, Direction Given, Angle from Reference, Elapsed Time, DirResponse, MouseXRes, MouseYRes");
            for (int j=0; j<records[i].gameDirections.Count; j++)
            {
                tw.WriteLine(",,,,,," + records[i].gameDirections[j].id + "," 
                    + records[i].gameDirections[j].source + "," 
                    + records[i].gameDirections[j].distanceToTarget + "," 
                    + records[i].gameDirections[j].direction + "," 
                    + records[i].gameDirections[j].landmark + ","
                    + records[i].gameDirections[j].angleToTarget + "," 
                    + records[i].gameDirections[j].timeElapsed +","
                    + records[i].gameDirections[j].timeSinceStart + "," 
                    + records[i].gameDirections[j].response.firstKeyStroke + "," 
                    + records[i].gameDirections[j].response.keyOnsetTime + ","
                    + records[i].gameDirections[j].response.firstMouseX + "," 
                    + records[i].gameDirections[j].response.firstMouseY + ","
                    + records[i].gameDirections[j].response.mouseOnsetTime);
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
            tw.WriteLine("PosX, PosY, PosZ, RotX, RotY, Sequence No., Current Trial, Time Since Game Start, Closer/Further");
        for (int i = 0; i < movement.Count; i++)
        {
            tw.WriteLine(movement[i].pos.x + "," 
                + movement[i].pos.y + "," 
                + movement[i].pos.z + "," 
                + movement[i].rotX + "," 
                + movement[i].rotY + "," 
                + movement[i].sequence + "," 
                + movement[i].currentTrial + "," 
                + movement[i].timeSinceStart + "," 
                + movement[i].closerOrFurther);
        }

        tw.Close();
    }

    public static void WriteDirections(string file, DataCollection records)
    {
        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);
        tw.WriteLine("Sequence, Trial no., Axis, Direction number, Source, Distance to Target, Direction Given, DirectionRef, Angle from Reference, Time into trial, Time Since Start, DirResponse, KeyOnset, MouseXRes, MouseYRes, MouseOnset");
        for (int i = 0; i < records.Count; i++)
        {
            for (int j = 0; j < records[i].gameDirections.Count; j++)
            {
                tw.WriteLine(records[i].sequenceNumber + "," 
                    + records[i].trialNumber + "," 
                    + records[i].axis + "," 
                    + records[i].gameDirections[j].id + "," 
                    + records[i].gameDirections[j].source + "," 
                    + records[i].gameDirections[j].distanceToTarget + "," 
                    + records[i].gameDirections[j].direction + "," 
                    + records[i].gameDirections[j].landmark + ","
                    + records[i].gameDirections[j].angleToTarget + "," 
                    + records[i].gameDirections[j].timeElapsed + "," 
                    + records[i].gameDirections[j].timeSinceStart + "," 
                    + records[i].gameDirections[j].response.firstKeyStroke + "," 
                    + records[i].gameDirections[j].response.keyOnsetTime + ","
                    + records[i].gameDirections[j].response.firstMouseX + "," 
                    + records[i].gameDirections[j].response.firstMouseY +","
                    + records[i].gameDirections[j].response.mouseOnsetTime);
            }
        }

        tw.Close();
    }

    public static void ExportGaze(List<Gazes> gaze)
    {
        //Export the data to csv
        if (!Directory.Exists(Application.streamingAssetsPath + "/Export"))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath + "/Export");
        }
        string path = Application.streamingAssetsPath + "\\Export\\";
        string exportFile = (DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")).ToString();
        string exportPath = path + exportFile + "-gaze.csv";
        ExportTrialData.WriteGaze(exportPath, gaze);
    }
    public static void WriteGaze(string file, List<Gazes> gaze)
    {

        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);
        tw.WriteLine("Time since trial start, AOI, gazeX, gazeY, gazeZ");
        for (int i = 0; i < gaze.Count; i++)
        {
            tw.WriteLine(gaze[i].TimeStamp + ","
                + gaze[i].AOI + ",");
                //+ gaze[i].gazeCoordinates.x + ","
                //+ gaze[i].gazeCoordinates.y + ","
                //+ gaze[i].gazeCoordinates.z);
        }

        tw.Close();
    }
}
