using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Battle_Order
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
            var fileInput = new FileStream();
            var binaryFormatter = new BinaryFormatter();
            String directory;

            try
            {
                fileInput = new FileStream("SaveDirectory", FileMode.Open, FileAccess.Read);
                directory = (String)binaryFormatter.Deserialize(fileInput);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No save directory was found for the monster database and parties.  Please select a save directory.", "No save directory", MessageBoxButtons.OK);
                var open = new OpenFileDialog();
                var result = open.ShowDialog();
            }
            finally
            {
                fileInput.Close();
            }

            return directory;
        }
    }
}