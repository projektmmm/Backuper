using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deamon
{
    public class Backuper
    {
        /// <summary>
        /// Zkopíruje soubory z Settings.SourcePath do Settings.DestinationPath
        /// Když soubory v destinaci existují, overwritne je to
        /// </summary>
        /// <author>
        /// Musílek
        /// </author>
        public void FullBackup()
        {
            int Count = 0;

            //Vytvoří podsložky
            foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath, Settings.DestinationPath));
            }

            //Zkopíruje všechny soubory a přepíše existující
            foreach (string newPath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(Settings.SourcePath, Settings.DestinationPath), true);
                Count++;
            }

            Console.WriteLine("FullBackup completed!");
            Console.WriteLine(Count + " files copied");
        }

        public void DifferentialBackup()
        {
            int Count = 0;

            //Vytvoří podsložky
            foreach (string dirPath in Directory.GetDirectories(Settings.SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(Settings.SourcePath, Settings.DestinationPath));
            }

            //Zjistí datum změny souboru v destinaci a porovná ho s datem změny source souboru, když je source soubor novější přepíše soubor v destinaci
            foreach (string newPath in Directory.GetFiles(Settings.SourcePath, "*.*", SearchOption.AllDirectories))
            {
                DateTime DestinationFile = File.GetLastWriteTime(newPath);
                DateTime SourceFile = File.GetLastWriteTime(newPath.Replace(Settings.SourcePath, Settings.DestinationPath));

                //je vetší - je starší
                if (DestinationFile > SourceFile)
                {
                    File.Copy(newPath, newPath.Replace(Settings.SourcePath, Settings.DestinationPath), true);
                    Count++;
                }
            }

            Console.WriteLine("DifferentialBackup completed!");
            Console.WriteLine(Count + " files copied");
        }
    }
}
