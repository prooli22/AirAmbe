//Nom: Olivier Provost
//Date: 2016-12-13
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;


namespace AirAmbe
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class EcranGuide : Window
    {
        public EcranGuide(int numPage)
        {
            InitializeComponent();

            string fileName = Environment.CurrentDirectory.GetFilePath("Documents\\Guide.xps");
            XpsDocument doc = new XpsDocument(fileName, FileAccess.Read);
            
            dvGuide.Document = doc.GetFixedDocumentSequence();

            dvGuide.GoToPage(numPage);
        }
    }

    public static class StringExtensions
    {
        public static string GetFilePath(this string EnvironmentCurrentDirectory, string FolderAndFileName)
        {
            //Split on path characters
            var CurrentDirectory =
                EnvironmentCurrentDirectory
                .Split("\\".ToArray())
                .ToList();
            //Get rid of bin/debug (last two folders)
            var CurrentDirectoryNoBinDebugFolder =
                CurrentDirectory
                .Take(CurrentDirectory.Count() - 2)
                .ToList();
            //Convert list above to array for Join
            var JoinableStringArray =
                CurrentDirectoryNoBinDebugFolder.ToArray();
            //Join and add folder filename passed in
            var RejoinedString =
                string.Join("\\", JoinableStringArray) + "\\";
            var final = RejoinedString + FolderAndFileName;
            return final;
        }
    }
}
