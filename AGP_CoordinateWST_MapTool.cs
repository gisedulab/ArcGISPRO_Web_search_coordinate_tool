using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
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
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AGP_CoordinateWST
{
    public static class Globals
    {
        public static String urlgoogle = "";

    }
    internal class AGP_CoordinateWST_MapTool : MapTool
    {
        public AGP_CoordinateWST_MapTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Screen;
            this.ContextMenuID = "AGP_CoordinateWST_AGP_CoordinateWST_MapTool_Menu";
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                e.Handled = true;
            if (e.ChangedButton == System.Windows.Input.MouseButton.Right)
            {
                QueuedTask.Run(() =>
                {
                    var mapPoint1 = MapView.Active.ClientToMap(e.ClientPoint);
                    SpatialReference SpatialReference2 = SpatialReferences.WGS84;
                    MapPoint mapPoint2 = GeometryEngine.Instance.Project(mapPoint1, SpatialReference2) as MapPoint;
                    var urllo = string.Format("{0:0.000000}", mapPoint2.X);
                    urllo = urllo.Replace(",", ".");
                    var urlla = string.Format("{0:0.000000}", mapPoint2.Y);
                    urlla = urlla.Replace(",", ".");
                    var urlgoogle1 = urlla + " " + urllo;
                    Globals.urlgoogle = urlgoogle1;
                });
            };
            //e.Handled = true;//Handle the event args to get the call to the corresponding async method
        }

        protected override Task HandleMouseDownAsync(MapViewMouseButtonEventArgs e)
        {
            return QueuedTask.Run(() =>
            {
                //Convert the clicked point in client coordinates to the corresponding map coordinates.
                var mapPoint1 = MapView.Active.ClientToMap(e.ClientPoint);
                //ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(string.Format("X: {0} Y: {1} Z: {2}",mapPoint.X, mapPoint.Y, mapPoint.Z), "Map Coordinates");
                var urlgoogle1 = "http://maps.google.com/maps?q=";
                SpatialReference SpatialReference1 = SpatialReferences.WGS84;
                MapPoint mapPoint2 = GeometryEngine.Instance.Project(mapPoint1, SpatialReference1) as MapPoint;
                var urllo = string.Format("{0:0.000000}", mapPoint2.X);
                urllo = urllo.Replace(",", ".");
                var urlla = string.Format("{0:0.000000}", mapPoint2.Y);
                urlla = urlla.Replace(",", ".");
                urlgoogle1 = urlgoogle1 + urlla + " " + urllo;
                Globals.urlgoogle = urlgoogle1;
                var urlgooglestart = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = true,
                     FileName = urlgoogle1
                };
                System.Diagnostics.Process.Start(urlgooglestart);
            });

        }

    }
}
