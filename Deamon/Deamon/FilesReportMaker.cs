using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Daemon
{
    public class FilesReportMaker
    {
        private List<FileInformation> Report = new List<FileInformation>();

        public List<FileInformation> GetReport()
        {
            return this.Report;
        }

        //přidá informace o filu do listu report
        public void AddFile(FileInfo file)
        {
            this.Report.Add(new FileInformation(file));
        }
    }
}
