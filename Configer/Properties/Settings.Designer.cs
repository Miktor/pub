﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17020
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Configer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />")]
        public global::System.Collections.Specialized.StringCollection SettingConfPathes {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["SettingConfPathes"]));
            }
            set {
                this["SettingConfPathes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>parsec.ini</string>
  <string>MDO.Parsec.Win.exe.config</string>
  <string>ParsecReplicatonClient.exe.config</string>
  <string>ParsecServiceHost.exe.config</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection SettingConfNames {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["SettingConfNames"]));
            }
            set {
                this["SettingConfNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>pTransport:ServerPath = pParsecServer:ServerAddress</string>
  <string>pTransport:ServerPort = pTransport:LocalPort</string>
  <string>ParsecServiceHost:authChannel = pParsecServer:ServerEntry</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CompareStrings {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CompareStrings"]));
            }
            set {
                this["CompareStrings"] = value;
            }
        }
    }
}