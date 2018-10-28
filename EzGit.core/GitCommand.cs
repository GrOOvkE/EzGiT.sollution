using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzGit.core
{
    public class GitCommand
    {

        public List<string> hostsList = new List<string>();

        


        public string ExecCommand(string path, string command )
        {
             Process gitStatus = new Process();
            string output;

            gitStatus.StartInfo.FileName = "cmd.exe";
            gitStatus.StartInfo.WorkingDirectory = path ;
            gitStatus.StartInfo.Arguments = command;
            gitStatus.StartInfo.UseShellExecute = false;
            gitStatus.StartInfo.RedirectStandardOutput = true;
            gitStatus.StartInfo.RedirectStandardError = true;
            gitStatus.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            gitStatus.Start();
           

            output = gitStatus.StandardOutput.ReadToEnd();

            output += gitStatus.StandardError.ReadToEnd();

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
