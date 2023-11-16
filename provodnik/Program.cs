using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Program
{

    public static void PrintMenu(DirectoryInfo directory, int chosenIndex)
    {
        Console.Clear();
        Console.WriteLine($"Current Directory: {directory.FullName}\n");

        var entries = directory.GetFileSystemInfos();

        Console.WriteLine("Directories and Files:");
        for (int i = 0; i < entries.Length; i++)
        {
            var entry = entries[i];
            string dateCreated = entry.CreationTime.ToString();

            if (i == chosenIndex)
            {
                Console.WriteLine($"> [{i}] {entry.Name}    Created on: {dateCreated}");
            }
            else
            {
                Console.WriteLine($"  [{i}] {entry.Name}    Created on: {dateCreated}");
            }
        }

        Console.WriteLine($"\nTotal Directories and Files: {entries.Length}");
    }

    private static void Main()
    {
        try
        {

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                long totalSpaceGB = drive.TotalSize / 1024 / 1024 / 1024;
                long freeSpaceGB = drive.AvailableFreeSpace / 1024 / 1024 / 1024;
                long usedSpaceGB = totalSpaceGB - freeSpaceGB;

                Console.WriteLine($"{drive.Name} - Used Space: {usedSpaceGB} GB");
            }

            Console.Write("Select drive: ");
            string selectedDriveName = Console.ReadLine();

            DriveInfo selectedDrive = drives.FirstOrDefault(d => d.Name == selectedDriveName);

            if (selectedDrive == null)
            {
                Console.WriteLine("Invalid drive selection.");
                return;
            }

            DirectoryInfo currentDirectory = selectedDrive.RootDirectory;
            ExploreDirectory(currentDirectory);
        }
        catch (Exception)
        {

            Console.WriteLine("ti popalsya na klicbate");
        }
    }

    private static void ExploreDirectory(DirectoryInfo directory)
    {
        int selectedItem = Arrow.Arrows(directory);

        if (selectedItem == -99)
            ExploreDirectory(directory.Parent);
        else
        {
            var entries = directory.GetFileSystemInfos();

            if (entries[selectedItem] is DirectoryInfo dir)
                ExploreDirectory(dir);
            else if (entries[selectedItem] is FileInfo file)
                OpenFile(file.FullName);
        }
    }

    private static void OpenFile(string filePath)
    {
        Console.WriteLine($"Opening {filePath}...");
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C start {filePath}";
        process.StartInfo = startInfo;
        process.Start();
    }
}