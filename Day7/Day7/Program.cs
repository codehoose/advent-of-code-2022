using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            FileStat root = LoadFileSystem(contents);

            // Part 1
            List<FileStat> dirs = new List<FileStat>();
            FindDirsSmallerThan(dirs, root, 100000);
            int total = dirs.Select(d => d.size)
                            .Aggregate((a, b) => a + b);

            Console.WriteLine($"Total bytes {total}");

            // Part 2
            List<FileStat> listOfDirs = new List<FileStat>();
            ListDirectories(listOfDirs, root);
            int totalSpaceUsed = listOfDirs.Select(d => d.size)
                                           .Aggregate((a, b) => a + b);
            int spaceLeft = 30000000 - (70000000 - root.size);

            FileStat dirToRemove = listOfDirs.OrderBy(d => d.size)
                                             .Where(d => d.size >= spaceLeft)
                                             .First();

            Console.WriteLine($"Largest directory is {dirToRemove.name} with {dirToRemove.size} bytes used");
            Console.WriteLine($"Space left {spaceLeft} bytes");
            Console.ReadLine();
        }

        static FileStat LoadFileSystem(string[] contents)
        {
            FileStat root = null;
            FileStat currentDir = null;

            int index = 0;
            while (index < contents.Length)
            {
                string line = contents[index];

                if (line.StartsWith("$"))
                {
                    // It's a command
                    // There are two commands - cd and ls
                    // Strip the command into the name of the command and any parameters
                    string cmdline = line.Substring(2); // Strip the leading '$ '
                    string[] cmdAndParams = cmdline.Split(" ".ToCharArray());
                    string cmd = cmdAndParams[0];
                    string parameter = cmdAndParams.Length == 2 ? cmdAndParams[1] : "";

                    if (cmd == "cd")
                    {
                        if (parameter != "..")
                        {
                            FileStat newDir = GetDirectory(currentDir, parameter);
                            newDir.parent = currentDir;
                            if (root == null)
                                root = newDir;

                            currentDir = newDir;
                        }
                        else
                        {
                            currentDir = currentDir.parent;
                        }

                        index++;
                    }
                    else if (cmd == "ls")
                    {
                        index++;
                        while (index < contents.Length && !contents[index].StartsWith("$"))
                        {
                            string fileEntry = contents[index];
                            if (fileEntry.StartsWith("dir"))
                            {
                                string dirName = fileEntry.Substring(4);
                                currentDir.dirs.Add(new FileStat(dirName, 0, true) { parent = currentDir });
                            }
                            else
                            {
                                string[] sizeAndName = fileEntry.Split(" ".ToCharArray());
                                int size = int.Parse(sizeAndName[0]);


                                // Walk back through directory to add the totals
                                FileStat temp = currentDir;
                                while (temp != null)
                                {
                                    temp.size += size;
                                    temp = temp.parent;
                                }

                                currentDir.files.Add(new FileStat(sizeAndName[1], size));
                            }

                            index++;
                        }
                    }
                }
            }

            return root;
        }

        static void ListDirectories(List<FileStat> dirs, FileStat dir)
        {
            dirs.Add(dir);
            foreach (var directory in dir.dirs)
            {
                ListDirectories(dirs, directory);
            }
        }

        static int GetActualSize(FileStat dir, int currentSize = 0)
        {
            int total = 0;
            foreach (var subDir in dir.dirs)
                total += GetActualSize(subDir, currentSize);

            foreach (var file in dir.files)
                total += file.size;
            return total;
        }

        static void FindDirsSmallerThan(List<FileStat> dirs, FileStat dir, int bytes)
        {
            if (dir.size <= bytes)
            {
                dirs.Add(dir);
            }

            foreach(var directory in dir.dirs)
            {
                FindDirsSmallerThan(dirs, directory, bytes);
            }
        }

        static FileStat GetDirectory(FileStat currentDir, string dirName)
        {
            if (currentDir == null)
            {
                return new FileStat(dirName, 0, true);
            }

            var found = currentDir.dirs.FirstOrDefault(d => d.name == dirName);
            return found ?? new FileStat(dirName, 0, true);
        }
    }

    class FileStat
    {
        public bool isDirectory;
        public int size;
        public string name;
        public FileStat parent;
        public List<FileStat> files = new List<FileStat>();
        public List<FileStat> dirs = new List<FileStat>();

        public FileStat(string name, int size = 0, bool isDirectory = false)
        {
            this.isDirectory = isDirectory;
            this.size = size;
            this.name = name;
        }
    }
}
