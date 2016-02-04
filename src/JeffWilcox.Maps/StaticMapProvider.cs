//
// Copyright (c) 2010-2011 Jeff Wilcox
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;

#if WINDOWS_APP
using Windows.Devices.Geolocation;
using System.Threading.Tasks;
using Windows.System;
#else
using System.Device.Location;
using System.Windows;
using Microsoft.Phone.Tasks;
#endif

// Super simple abstract interface. I could consider exposing an actual
// StaticMapProvider property on the StaticMap control, but that would be more
// verbose in XAML and most people wouldn't need that.

namespace JeffWilcox.Controls
{
    public abstract class StaticMapProvider
    {
#if WINDOWS_APP
        public Geopoint Center { get; set;}
#else
        public GeoCoordinate Center { get; set;}
#endif

        public int Height { get; set; }

        public int Width { get; set; }

        public string Path { get; set; }

        public int ZoomLevel { get; set; }

        public bool IsSensor { get; set; }
        
        public string ProviderKey { get; set; }

        public StaticMapMode MapMode { get; set; }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public virtual void Validate()
        {
            if (ZoomLevel > 21 || ZoomLevel < 1)
            {
                // throw new ArgumentException("ZoomLevel must be between 1 and 21.");
            }
        }



#if WINDOWS_APP

        public async Task<bool> NavigateBrowserToMap()
        {
            try
            {
                var uri = GetWebBrowserMap();
                await Launcher.LaunchUriAsync(uri);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> NavigateToMapsApplication()
        {
            try
            {
                //https://msdn.microsoft.com/en-US/library/windows/apps/xaml/dn614996.aspx

#if WINDOWS_APP
                var latitude = Center.Position.Latitude;
                var longitude = Center.Position.Longitude;

                // Launch the URI
                string uri = string.Format("{0}cp={1:N5}~{2:N5}&lvl={3}", "bingmaps:?", latitude, longitude, ZoomLevel);

                var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(uri));

#else
                var latitude = Center.Latitude;
                var longitude = Center.Longitude;

                // The URI to launch
                string uriToLaunch = string.Format(@"bingmaps:?cp={0}~-{1}&lvl={2}", longitude, latitude, ZoomLevel);
                var uri = new Uri(uriToLaunch);

                // Launch the URI
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
#endif
                return success;
            }
            catch (Exception)
            {
                return false;
            }

            
        }

#else

                public bool NavigateBrowserToMap()
        {
            try
            {
                WebBrowserTask wbt = new WebBrowserTask();
                wbt.Uri = GetWebBrowserMap();
                wbt.Show();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool NavigateToMapsApplication()
        {
            try
            {
                BingMapsTask bmt = new BingMapsTask();
                bmt.ZoomLevel = ZoomLevel;
                bmt.Center = Center;
                bmt.Show();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

#endif
        protected void RequireCenter()
        {
            if (Center == null)
            {
                throw new ArgumentException("Center is null.");
            }
        }

        public virtual Uri GetWebBrowserMap()
        {
            throw new NotImplementedException();
        }

        public virtual Uri GetStaticMap()
        {
            throw new NotImplementedException();
        }

        protected int Clamp(int max, int value)
        {
            return value > max ? max : value;
        }
    }
}
