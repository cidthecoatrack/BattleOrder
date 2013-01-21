using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace BattleOrder
{
    public class FileAccessor
    {
        private String saveDirectory;
        private FileStream input;
        private BinaryFormatter binary;
        
        public FileAccessor(String saveDirectory)
        {
            this.saveDirectory = saveDirectory;
        }

        public static String GetSaveDirectoryFromWorkingDirectory()
        {
            var directory = String.Empty;

            try
            {
                var fileStream = new FileStream("SaveDirectory", FileMode.Open, FileAccess.Read);
                var binaryFormatter = new BinaryFormatter();

                directory = Convert.ToString(binaryFormatter.Deserialize(fileStream));

                fileStream.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No save directory was found for the monster database and parties.  Please select a save directory.", "No save directory", MessageBoxButtons.OK);

                directory = GetDirectoryFromDialog();

                var fileStream = new FileStream("SaveDirectory", FileMode.Create, FileAccess.Write);
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, directory);
                fileStream.Close();
            }

            return directory;
        }

        private static String GetDirectoryFromDialog()
        {
            var folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();
            return folderBrowser.SelectedPath;
        }
    }
}