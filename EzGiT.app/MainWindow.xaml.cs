using EzGit.core;
using System;
using System.Linq;
using System.Windows;



namespace EzGiT.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GitCommand gitCommand = new GitCommand();

        string path;

        public MainWindow()
        {
            InitializeComponent();
            btnStatus.IsEnabled = false;
            btnGitInit.IsEnabled = false;
            btnGitClone.IsEnabled = false;
            btnGitCommit.IsEnabled = false;
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            string output = $"{gitCommand.ExecCommand(path,"/c git status")}";

            if (output == "")
            {
                txtOutput.Text = "Kies een geldige Git Repo";
            }
            else
            {
                txtOutput.Text = output;
            }
        }


        private void btnChooseDir_Click(object sender, RoutedEventArgs e)
        {
            path = gitCommand.GetPath();
            
            if (path != null)
            {
                btnStatus.IsEnabled = true;
                btnGitInit.IsEnabled = true;
                btnGitClone.IsEnabled = true;
                lblWorkingDir.Content = path;
            }
        }


        private void btnGitInit_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text  = $"{gitCommand.ExecCommand(path, "/c git init")}";
        }


        private void btnGitClone_Click(object sender, RoutedEventArgs e)
        {
            string repourl = tbRepoToClone.Text;

            if (Uri.IsWellFormedUriString(repourl, UriKind.Absolute))
            {

                txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git clone " + repourl)}" ;
                path =  $"{path}\\{repourl.Split('/').Last().Replace(".git","")}";
                lblWorkingDir.Content = path;


            }
            else
            {
                txtOutput.Text = "INVALID URL";
            }
               
        }

        private void btnGitCommit_Click(object sender, RoutedEventArgs e)
        {

            if (gitCommand.ExecCommand(path, "/c git status").Contains("nothing to commit"))
            {
                string linebreak = "-----------------------------------";
                txtOutput.Text = $"NOTING TO COMMIT\n{linebreak}\n EXTRA NFO:\n{linebreak}\n {gitCommand.ExecCommand(path, "/c git status")}";

            }
            else
            {
                string commitmsg = tbCommitMessage.Text;

                if (commitmsg == "")
                {
                    txtOutput.Text = "ENTER COMMITMSG";
                }
                else
                {
                   
                    txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git commit -m " + commitmsg)}";
                }

            }
    
            
        }

        private void btnGitPush_Click(object sender, RoutedEventArgs e)
        {

            txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git push origin master")}";

        }

        private void btnGitStageFiles_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = gitCommand.ExecCommand(path, "/c git add * ");
            txtOutput.Text = "STAGED";
            btnGitCommit.IsEnabled = true;
        }
    }
}
