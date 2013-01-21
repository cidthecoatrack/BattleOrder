using System;
using System.IO;
using Moq;
using NUnit.Framework;

namespace BattleOrder.Tests
{
    [Test]
    public class FileAccessorTests
    {
        [Test]
        public void GetsCorrectDirectoryFromSaveDirectoryFile()
        {
            File.WriteAllText("SaveDirectory", "directory");
            var directory = FileAccessor.GetSaveDirectoryFromWorkingDirectory();
            Assert.That(directory, Is.EqualTo("directory"));
        }
    }
}