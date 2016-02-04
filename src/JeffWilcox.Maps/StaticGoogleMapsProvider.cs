//
// Copyright (c) 2012 Jeff Wilcox
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
using System.Globalization;

//
// Important API Information:
// http://code.google.com/apis/maps/documentation/staticmaps/
//
// If you use the Google Maps static map provider, please confirm that you are
// using the control appropriately and within the API guidelines for the
// service.
//
// Within your app:
// ---
// One interesting implementation note: per the terms of the Google Maps
// Static API is that you must offer a link on the phone to the web browser 
// pointing at the Google Maps page.
//
// You can accomplish this using the NavigateBrowserToMap() method, and should
// offer this through something such as an application bar menu item. This
// seems to be required for Google Maps!
//

namespace JeffWilcox.Controls
{
    public class StaticGoogleMapsProvider : StaticMapProvider
    {
        private const string StaticMapsUrlFormat =
            "http://maps.googleapis.com/maps/api/staticmap?center=" +
            "{0},{1}" + // lat,long
            "&zoom={2}" + // zoomLevel // int from 1 to 22, 15 seems normal-ish
            "&size=" +
            "{3}x{4}" + // width, height
            "&sensor={5}" + // dev key
            "&maptype={6}"; // map type

        private const string StaticMapsUrlFormatWithPath =
            "http://maps.googleapis.com/maps/api/staticmap?" +
            "size=" +
            "{0}x{1}" + // width, height
            "&sensor={2}" + // dev key
            "&maptype={3}" + // map type
            "&path=color:{4}" + //path color
            "|weight:{5}" + //path weight
            "|{6}"; //path

        public override Uri GetStaticMap()
        {
            // The free tier for static Google maps provides a max 640x640px.
            int width = Clamp(640, Width);
            int height = Clamp(640, Height);

            // If people have Google Maps API for Business then I need to look
            // in to providing an appropriate API (or perhaps a premium GOOG
            // provider class) for that.

#if WINDOWS_APP
            var latitiude = Center.Position.Latitude;
            var longitude = Center.Position.Longitude;
#else
            var latitiude = Center.Latitude;
            var longitude = Center.Longitude;
#endif

            var format = StaticMapsUrlFormat;

            string uriString;
            if (string.IsNullOrEmpty(Path))
            {

                uriString = string.Format(
                    CultureInfo.InvariantCulture,
                    format,
                    latitiude,
                    longitude,
                    BingMapsHelper.ClampZoomLevel(ZoomLevel),
                    width,
                    height,
                    IsSensor ? "true" : "false",
                    TranslateMapMode(this.MapMode));
            }
            else
            {
                
                format = StaticMapsUrlFormatWithPath;

                uriString = string.Format(
                    CultureInfo.InvariantCulture,
                    format,
                    width,
                    height,
                    IsSensor ? "true" : "false",
                    TranslateMapMode(this.MapMode),
                    "red",
                    "5",
                    Path);
            }

            var uri = new Uri(uriString, UriKind.Absolute);

            return uri;
        }

        private string TranslateMapMode(StaticMapMode mode)
        {
            string mapType = string.Empty;

            switch (mode)
            {
                case StaticMapMode.Map:
                    mapType = "roadmap";
                    break;
                case StaticMapMode.Satellite:
                    mapType = "satellite";
                    break;
                case StaticMapMode.Hybrid:
                    mapType = "hybrid";
                    break;
                case StaticMapMode.Terrain:
                    mapType = "terrain";
                    break;
                default:
                    mapType = "roadmap";
                    break;
            }

            return mapType;
        }

        public override Uri GetWebBrowserMap()
        {
            RequireCenter();

#if WINDOWS_APP
            var latitiude = Center.Position.Latitude;
            var longitude = Center.Position.Longitude;
#else
            var latitiude = Center.Latitude;
            var longitude = Center.Longitude;
#endif

            return new Uri(string.Format(
                CultureInfo.InvariantCulture,
                "http://maps.google.com/?q={0},{1}",
                latitiude,
                longitude),
                UriKind.Absolute);
        }
    }
}
