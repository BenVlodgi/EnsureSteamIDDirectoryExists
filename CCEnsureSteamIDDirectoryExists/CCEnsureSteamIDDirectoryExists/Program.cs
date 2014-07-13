using System;
using System.IO;
using System.Linq;

namespace CCEnsureSteamIDDirectoryExists
{
    class Program
    {
        static void Main()
        {
            string problem = string.Empty;
            // Traverse up out of the bin directory, then navigate to aperturetag\puzzles, copy the names of all the directories in there, and make new directories with the same names in aperturetag\portal2\maps\puzzlemaker
            var apertureTag = Directory.GetParent(Directory.GetCurrentDirectory());

            var puzzles = apertureTag.GetDirectories("puzzles").FirstOrDefault();
            if (puzzles == null) { LogProblem("Failed to find 'aperturetag\\puzzles'"); return; }

            var portal2 = apertureTag.GetDirectories("portal2").FirstOrDefault();
            if (portal2 == null) { LogProblem("Failed to find 'aperturetag\\portal2'"); return; }
            var maps = portal2.GetDirectories("maps").FirstOrDefault();
            if (maps == null) { LogProblem("Failed to find 'aperturetag\\portal2\\maps'"); return; }
            var puzzlemaker = maps.GetDirectories("puzzlemaker").FirstOrDefault();
            if (puzzlemaker == null) { LogProblem("Failed to find 'aperturetag\\portal2\\maps\\puzzlemaker'"); return; }

            foreach (var directory in puzzles.EnumerateDirectories())
                if (puzzlemaker.EnumerateDirectories().Where(d => d.Name == directory.Name).FirstOrDefault() == null)
                    puzzlemaker.CreateSubdirectory(directory.Name);

        }
        private static void LogProblem(string problem)
        {
            File.AppendAllText("CCEnsureSteamIDDirectoryExists.errors.txt", string.Format("{0}\n{1}\n\n", DateTime.Now.ToString(), problem));
        }
    }
}
