using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kvdemy.Core.Enums
{
    public enum MessageType
    {
        Text = 1,
        Image = 2,
        Video = 3,
        File = 4
    }
}