using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MSMQ.Helper
{
    public class Enums
    {
        public enum MessageFormatter
        {
            BinaryFormatter,
            XmlMessageFormatter,
            JsonMessageFormatter,
        }
    }
}
