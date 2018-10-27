using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzGit.core
{
    public class GitCommand
    {

        

        

        



        public string ExecCommand(string path, string command )
        {
            Process gitStatus = new Process();

            gitStatus.StartInfo.FileName = "cmd.exe";
            gitStatus.StartInfo.WorkingDirectory = path ;
            gitStatus.StartInfo.Arguments = command;
            gitStatus.StartInfo.UseShellExecute = false;
            gitStatus.StartInfo.RedirectStandardOutput = true;
            gitStatus.Start();

            string output = gitStatus.StandardOutput.ReadToEnd();


            
            return output;
        }




        public string GetPath()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string path = dialog.SelectedPath;
            return path;
        }

        
    
       

    }
}
