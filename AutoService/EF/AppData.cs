using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.EF
{
    public class AppData
    {
        public static AutoserviceEntities1 Context { get;} = new AutoserviceEntities1();
    }
}
