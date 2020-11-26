using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.EF
{
    public class AppData
    {
        public static AutoserviceEntities Context { get;} = new AutoserviceEntities();
    }
}
