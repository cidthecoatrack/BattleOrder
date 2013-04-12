using System;
using System.IO;

namespace BattleOrder.Repository.Gateways
{
    public class SaveDirectoryGateway
    {
        public String GetSaveDirectoryFromWorkingDirectory()
        {
            return File.ReadAllText("SaveDirectory");
        }

        public void MakeSaveDirectoryFile(String saveDirectory)
        {
            File.WriteAllText("SaveDirectory", saveDirectory);
        }
    }
}