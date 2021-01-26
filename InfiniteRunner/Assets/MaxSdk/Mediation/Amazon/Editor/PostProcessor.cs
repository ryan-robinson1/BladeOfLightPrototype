//
//  PostProcessor.cs
//  AppLovin MAX Unity Plugin
//
//  Created by Andrew Tian on 01/05/21.
//  Copyright © 2021 AppLovin. All rights reserved.
//

#if UNITY_IPHONE || UNITY_IOS

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace AppLovinMax.Mediation.Amazon.Editor
{
    public class PostProcessor
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            var plistPath = Path.Combine(buildPath, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            if (plist.root.values.ContainsKey("NSCalendarsUsageDescription")) return;

            // Set the NSCalendarsUsageDescription in the plist.
            plist.root.SetString("NSCalendarsUsageDescription", "Used to deliver better advertising experience");

            // Write the file with the updated settings.
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}

#endif
