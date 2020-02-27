using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public enum BookingStatus
    {
        Pending ,
        Approved ,
        Rejected 
    }

    public enum Places
    {
        Secunderabad=0,
        Miyapur,
        Madhapur,
        Durgamcheruvu,
        Lingampally,
        Kothaguda,
        Kondapur,
        Hitechcity
    }

    public enum CarType
    {
        [Description("Prime SUV")]
        PrimeSuv,
        [Description("Prime Play")]
        PrimePlay,
        [Description("Prime Exec")]
        PrimeExec,
        [Description("PrimeSeldon")]
        PrimeSeldon,
        [Description("mini")]
        mini,
        [Description("micro")]
        Micro
    }
}
