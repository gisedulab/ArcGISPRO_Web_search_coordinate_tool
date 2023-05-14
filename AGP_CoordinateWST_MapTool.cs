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
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AGP_CoordinateWST
{
    internal class AGP_CoordinateWST_MapTool : MapTool
    {
        public AGP_CoordinateWST_MapTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Screen;
            this.ContextMenuID = "AGP_CoordinateWST_AGP_CoordinateWST_MapTool_Menu";
        }

        protected override Task OnToolActivateAsync(bool hasMapViewChanged)
        {
            //Find settings
            string settingsPath = System.IO.Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ArcGIS", "AddIns", "ArcGISPro", "AGP_CoordinateWST.txt");
            if (File.Exists(settingsPath))
            {
                //Read settings
                StreamReader sr = new StreamReader(settingsPath);
                //Read the first line of text
                Globals.AGP_WS_Settingsline1 = sr.ReadLine();
                if (Globals.AGP_WS_Settingsline1.Contains("http://") is false) {Globals.AGP_WS_Settingsline1 = "http://maps.google.com/maps?q=LATITUDE LONGITUDE"; }
                //Read the second line of text
                while (sr.Peek() >= 0)
                {
                    Globals.AGP_WS_Settingsline2 = sr.ReadLine();
                }
                //close the file
                sr.Close();
                if (Globals.AGP_WS_Settingsline2.Contains("http://") is false) { Globals.AGP_WS_Settingsline2 = "http://map.baidu.com/?latlng=LATITUDE,LONGITUDE"; } 
            }
            else
            {
                //Write settings
                StreamWriter sw = new StreamWriter(settingsPath);
                sw.WriteLine("http://maps.google.com/maps?q=LATITUDE LONGITUDE");
                Globals.AGP_WS_Settingsline1 = "http://maps.google.com/maps?q=LATITUDE LONGITUDE";
                sw.WriteLine("http://map.baidu.com/?latlng=LATITUDE,LONGITUDE");
                Globals.AGP_WS_Settingsline2 = "http://map.baidu.com/?latlng=LATITUDE,LONGITUDE";
                //close the file
                sw.Close();
            }
            return base.OnToolActivateAsync(hasMapViewChanged);
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
                    Globals.urlgoogleLONGITUDE = urllo;
                    Globals.urlgoogleLATITUDE = urlla;
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
                var urlgoogle1 = Globals.AGP_WS_Settingsline1;
                SpatialReference SpatialReference1 = SpatialReferences.WGS84;
                MapPoint mapPoint2 = GeometryEngine.Instance.Project(mapPoint1, SpatialReference1) as MapPoint;
                var urllo = string.Format("{0:0.000000}", mapPoint2.X);
                urllo = urllo.Replace(",", ".");
                var urlla = string.Format("{0:0.000000}", mapPoint2.Y);
                urlla = urlla.Replace(",", ".");
                urlgoogle1 = urlgoogle1.Replace("LONGITUDE", urllo);
                urlgoogle1 = urlgoogle1.Replace("LATITUDE", urlla);
                Globals.urlgoogleLONGITUDE = urllo;
                Globals.urlgoogleLATITUDE = urlla;
                Globals.urlgoogle = urlla + "" + urllo;
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
