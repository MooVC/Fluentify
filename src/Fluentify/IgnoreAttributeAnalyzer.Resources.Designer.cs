﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fluentify {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class IgnoreAttributeAnalyzer_Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal IgnoreAttributeAnalyzer_Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Fluentify.IgnoreAttributeAnalyzer.Resources", typeof(IgnoreAttributeAnalyzer_Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This property is not considered by Fluentify because the Fluentify attribute has not been applied to the type, so the usage of the Ignore attribute is redundant..
        /// </summary>
        internal static string MissingFluentifyRuleDescription {
            get {
                return ResourceManager.GetString("MissingFluentifyRuleDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; is not considered by Fluentify, making the usage of the Ignore attribute redundant..
        /// </summary>
        internal static string MissingFluentifyRuleMessageFormat {
            get {
                return ResourceManager.GetString("MissingFluentifyRuleMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type does not utilize Fluentify.
        /// </summary>
        internal static string MissingFluentifyRuleTitle {
            get {
                return ResourceManager.GetString("MissingFluentifyRuleTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This property is not considered by Fluentify, so the usage of the Ignore attribute is redundant..
        /// </summary>
        internal static string RedundantUsageRuleDescription {
            get {
                return ResourceManager.GetString("RedundantUsageRuleDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; is not considered by Fluentify, making the usage of the Ignore attribute redundant..
        /// </summary>
        internal static string RedundantUsageRuleMessageFormat {
            get {
                return ResourceManager.GetString("RedundantUsageRuleMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property is already disregarded from consideration by Fluentify.
        /// </summary>
        internal static string RedundantUsageRuleTitle {
            get {
                return ResourceManager.GetString("RedundantUsageRuleTitle", resourceCulture);
            }
        }
    }
}
