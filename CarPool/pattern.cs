using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool
{
    class Patterns
    {
        public const string CarNumber = @"[0-9a-zA-z]+";
        public const string EmailAddress = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$";
        public const string Name = @"[a-zA-Z]+";
        public const string PhoneNumber = @"(?=.*?\d{3}( |-|.)?\d{4})((?:\+?(?:1)(?:\1|\s*?))?(?:(?:\d{3}\s*?)|(?:\((?:\d{3})\)\s*?))\1?(?:\d{3})\1?(?:\d{4})(?:\s*?(?:#|(?:ext\.?))(?:\d{1,5}))?)\b";
        public const string CarCapacity = @"[3-7]";
        public const string Date = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(20)\d\d$";
        public const string Time = @"^([01][0-9]|2[0-3]):([0-5][0-9])";
        public const string Amount = @"^[1-9](0-9)*(\.[0-9]+)?";
        public const string Text = @"^[a-zA-Z0-9]+";
        public const string Password = @"^[a-zA-Z0-9@#$%\*]{4,16}";
    }
}
