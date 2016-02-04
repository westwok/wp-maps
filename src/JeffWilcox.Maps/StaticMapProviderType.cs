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

// Google Maps is a trademark of Google, Inc.
// Bing Maps is a trademark of Microsoft Corporation

#if WINDOWS_APP
using System.ComponentModel.DataAnnotations;
#else
using System.ComponentModel;
#endif

namespace JeffWilcox.Controls
{
    /// <summary>
    /// Represents an Internet static maps provider.
    /// </summary>
    public enum StaticMapProviderType
    {
        /// <summary>
        /// Bing Maps.
        /// </summary>
#if WINDOWS_APP
        [Display(Description = "Bing Maps")]
#else
        [Description("Bing Maps")]
#endif
        Bing,

        /// <summary>
        /// Google Maps.
        /// </summary>
#if WINDOWS_APP
        [Display(Description = "Google Maps")]
#else
        [Description("Google Maps")]
#endif
        Google,
        
        /// <summary>
        /// MapQuest.
        /// </summary>
#if WINDOWS_APP
        [Display(Description = "MapQuest")]
#else
        [Description("MapQuest")]
#endif
        MapQuest,
        
        /// <summary>
        /// OpenStreetMap.
        /// </summary>
#if WINDOWS_APP
        [Display(Description = "OpenStreetMap")]
#else
        [Description("OpenStreetMap")]
#endif
        OpenStreetMap,

#if WINDOWS_APP
        [Display(Description = "Nokia Maps")]
#else
        [Description("Nokia Maps")]
#endif
        Nokia,
    }
}
