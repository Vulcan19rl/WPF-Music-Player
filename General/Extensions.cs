using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Music_Player.General
{
    public static class Extensions
    {

    
            public static TimeSpan StripMilliseconds(this TimeSpan time)
            {
                return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
            }
        
    }
}
