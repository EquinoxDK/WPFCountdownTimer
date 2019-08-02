using CountdownShared.ViewModels;
using System;
using System.Text;

namespace CountdownGUI.Helpers
{
    class SaveHelper
    {
        static private bool CanSave = true;

        static public void SaveList(TimerSheetViewModel[] sheets)
        {
            if (CanSave)
            {
                CanSave = false;
                string filename = @"timer.txt";
                string content = "";
                StringBuilder sb = new StringBuilder();
                foreach (TimerSheetViewModel item in sheets)
                {
                    sb.AppendLine($"{item.TimerSheet.Text} {item.TimerSheet.Time}");
                }
                content = sb.ToString();

                try
                {
                    string oldContent = System.IO.File.ReadAllText(filename);
                    if (!content.Equals(oldContent))
                    {
                        System.IO.File.WriteAllText(filename, content);
                    }
                }
                catch (Exception)
                {

                }
                CanSave = true;
            }
        }

    }
}
