using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backuperApi
{
    public class AddDaemon
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DaemonTo { get; set; }
        //UserId:number,
        //Name:string,
        //DaemonTo:string,
        //Description:string,
    }
}