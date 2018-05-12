using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class ReportMaker
    {
        private List<FileInformation> Report = new List<FileInformation>();
        private List<ErrorDetails> ErrorDetails = new List<ErrorDetails>();

        public List<FileInformation> GetReport()
        {
            return this.Report;
        }

        //přidá informace o filu do listu report
        public void AddFile(FileInfo file)
        {
            this.Report.Add(new FileInformation(file));
        }

        public void AddError(ErrorDetails er)
        {
            this.ErrorDetails.Add(er);
        }
    }
}