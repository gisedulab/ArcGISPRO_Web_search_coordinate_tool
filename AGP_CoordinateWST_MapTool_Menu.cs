using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.Topology;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AGP_CoordinateWST
{
    internal class AGP_CoordinateWST_MapTool_Menu_button1 : Button
    {
        protected override void OnClick()
        {
            var urlgooglestart = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "https://www.bing.com/maps?q=" + Globals.urlgoogle
            };
            System.Diagnostics.Process.Start(urlgooglestart);
        }
        
        
    }

    internal class AGP_CoordinateWST_MapTool_Menu_button2 : Button
    {
        protected override void OnClick()
        {
            var urlgooglestart = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "https://www.openstreetmap.org/search?query=" + Globals.urlgoogle
            };
            System.Diagnostics.Process.Start(urlgooglestart);
        }
    }

}
