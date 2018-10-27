using EzGit.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;



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
    }
}
