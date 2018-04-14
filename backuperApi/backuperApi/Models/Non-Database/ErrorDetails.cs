﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class ErrorDetails
    {
        public int Id { get; set; }
        public int AffectedFiles { get; set; }
        public string Problem { get; set; }
        public int BackupId { get; set; }
        public int DaemonId { get; set; }
        public string DaemonName { get; set; }
        public bool Solved { get; set; }
        public string ProblemPath { get; set; }

        public static string GetQuery()
        {
            return "SELECT be.Id, be.AffectedFiles, be.Problem, br.BackupId, da.Id, da.Name, be.Solved, be.ProblemPath " +
                "FROM BackupErrors be INNER JOIN " +
                "BackupReport br ON be.Id=br.BackupErrorId " +
                "INNER JOIN Daemons da ON da.Id=br.DaemonId " +
                "INNER JOIN Users us ON  br.UserId=us.Id " +
                $"WHERE us.Username=@UserName AND br.DaemonId=@DaemonId " +
                "AND be.Problem <> 'No problem'" +
                "ORDER BY be.Solved ASC";
        }
    }
}