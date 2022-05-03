using System.IO;

public static class ExportTrialData
{
    public static void Write(string file, DataCollection records)
    {

        TextWriter tw = new StreamWriter(file, false);

        tw.Close();

        tw = new StreamWriter(file, true);

        for(int i=0; i< records.Count; i++)
        {
            tw.WriteLine("Trial Number, Axis, Number of Instructions, Time Taken, Trial Success, Game Directions");
            tw.WriteLine(records[i].trialNumber+","+ records[i].axis + "," + records[i].numberOfDirections + ","+ records[i].timeTaken + "," + records[i].success.ToString() + ",Direction number, Distance to Target, Direction Given, Angle from Reference, Elapsed Time, DirResponse, MouseXRes, MouseYRes");
            //tw.Write(",,,,,Direction number, Distance to Target, Direction Given, Angle from Reference, Elapsed Time, DirResponse, MouseXRes, MouseYRes");
            for (int j=0; j<records[i].gameDirections.Count; j++)
            {
                tw.WriteLine(",,,,," + records[i].gameDirections[j].id + "," + records[i].gameDirections[j].distanceToTarget + "," + records[i].gameDirections[j].direction + "," + records[i].gameDirections[j].angleToTarget + "," + records[i].gameDirections[j].timeElapsed + "," + records[i].gameDirections[j].response.firstKeyStroke + "," + records[i].gameDirections[j].response.firstMouseX + "," + records[i].gameDirections[j].response.firstMouseY);
                //if (records[i].gameDirections[j].response != null)
                //{
                //    tw.WriteLine(",,,,,,,,,," + records[i].gameDirections[j].response.firstKeyStroke + "," + records[i].gameDirections[j].response.firstMouseX + "," + records[i].gameDirections[j].response.firstMouseY);
                //}
                
            }
        }

        tw.Close();
    }


}
