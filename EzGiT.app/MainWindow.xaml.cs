using EzGit.core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            grdClone.IsEnabled = false;
            btnGitCommit.IsEnabled = false;
            btnGitStageFiles.IsEnabled = false;
            btnGitPush.IsEnabled = false;
           
            tbCommitMessage.IsEnabled = false;
            btnGitPull.IsEnabled = false;
            btnExpertCmd.IsEnabled = false;
            btnChooseDir.Background = Brushes.Red;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            string output = $"{gitCommand.ExecCommand(path, "/c git status")}";

            if (output.Contains("fatal")|| output == "")
            {
                txtOutput.Text = $"GIT OUTPUT:\n{output}\n ------------------\n Kies een geldige Git Repo ... \n of... \n Init een nieuwe Repository\n of \n Clone een  online Repository  ";
                btnGitInit.IsEnabled = true;
             
                btnStatus.Background = Brushes.Transparent;
                btnGitInit.Background = Brushes.Red;
                btnGitClone.Background = Brushes.Red;
                btnChooseDir.Background = Brushes.Red;
                grdClone.IsEnabled = true;

            }
            else
            {
                txtOutput.Text = output;

                btnExpertCmd.IsEnabled = true;
                btnGitStageFiles.IsEnabled = true;
                btnGitPush.IsEnabled = true;
                btnGitPull.IsEnabled = true;
                btnChooseDir.Background = Brushes.Transparent;
                
            }
        }


        private void btnChooseDir_Click(object sender, RoutedEventArgs e)
        {
            path = gitCommand.GetPath();
            
            if (path != null)
            {
                btnStatus.IsEnabled = true;
                btnGitPull.IsEnabled = false;
              
                string commitcount = gitCommand.ExecCommand(path, "/c git rev-list --all --count");
                if (commitcount.Contains("fatal"))
                {
                    lblWorkingDir.Content = "";
                    lblWorkingPath.Content = "";
                    lblCommitsCount.Content = "";
                    btnChooseDir.Background = Brushes.Transparent;
                    btnStatus.Background = Brushes.Red;
                    

                }
                else
                {
                    lblWorkingDir.Content = path.Split('\\').Last();
                    lblWorkingPath.Content = path;
                    lblCommitsCount.Content = commitcount;
                    btnGitInit.Background = Brushes.Transparent;
                    btnGitClone.Background = Brushes.Transparent;
                    btnChooseDir.Background = Brushes.Transparent;
                    txtOutput.Text = "PRESS STATUS BUTTON";
                    btnStatus.Background = Brushes.Red;
                    grdClone.IsEnabled = false;
                    btnGitInit.IsEnabled = false;
                }
                
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

                string output = gitCommand.ExecCommand(path, "/c git clone " + repourl) ;
                path =  $"{path}\\{repourl.Split('/').Last().Replace(".git","")}";
                lblWorkingDir.Content = repourl.Split('/').Last().Replace(".git", "") ;
                lblWorkingPath.Content = path;

                if (output.Contains("Cloning into"))
                {
                    txtOutput.Text = $"GIT OUTPUT: \n {output} \n  ------------------------ \n CLONING DONE";

                }
                else txtOutput.Text = output;

            }
            else
            {
                txtOutput.Text = "INVALID URL";
            }
               
        }

        private void btnGitCommit_Click(object sender, RoutedEventArgs e)
        {

                string commitmsg = tbCommitMessage.Text;

                if (commitmsg == "")
                {
                    txtOutput.Text = "ENTER COMMITMSG";
                }
                else
                {
                   
                    txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git commit -m " + commitmsg)}";
                    btnGitCommit.Background = Brushes.Transparent;
                    btnGitPush.Background = Brushes.Red;
            }
      
        }

        private void btnGitPush_Click(object sender, RoutedEventArgs e)
        {

            txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git push origin master")}";
            btnGitPush.Background = Brushes.Transparent;

        }

        private void btnGitStageFiles_Click(object sender, RoutedEventArgs e)
        {
            string output = gitCommand.ExecCommand(path, "/c git add * ");
       
            btnGitCommit.IsEnabled = true;
            tbCommitMessage.IsEnabled = true;
            btnGitCommit.Background = Brushes.Red;

            txtOutput.Text = $" {output}\n{gitCommand.ExecCommand(path, "/c git status")}" ;
        }

        private void btnGitPull_Click(object sender, RoutedEventArgs e)
        {       
                txtOutput.Text = gitCommand.ExecCommand(path, "/c git pull ");
        }

        private void btnExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized ;
        }

        private void btnGitLog_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git log")}";

        }

        private void btnGitChangeLog_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git log --graph --oneline --decorate --all")}";
        }

        private void btnGitRemotes_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = $"{gitCommand.ExecCommand(path, "/c git remote -v")}";
        }

        private void btnGitConfig_Click(object sender, RoutedEventArgs e)
        {
           string output = gitCommand.ExecCommand(path, $"/c git config --global --replace-all user.name {txtGitUserName.Text}");
            output += $"\n________\n{gitCommand.ExecCommand(path, $"/c git config --global --replace-all user.name {txtGitUserName.Text}")}";


            if (output == "")
            {
                txtOutput.Text = "UPDATE COMPLETE";
            }

            txtOutput.Text = output;
        }
    }
}
