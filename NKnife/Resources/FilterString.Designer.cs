﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NKnife.Resources {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class FilterString {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FilterString() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NKnife.Resources.FilterString", typeof(FilterString).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
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
        ///   查找类似 All Files|*.* 的本地化字符串。
        /// </summary>
        internal static string All {
            get {
                return ResourceManager.GetString("All", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Image Files|*.bmp;*.jpg;*.ico;*.icon;*.png;*.gif;|All Files|*.* 的本地化字符串。
        /// </summary>
        internal static string Image {
            get {
                return ResourceManager.GetString("Image", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.* 的本地化字符串。
        /// </summary>
        internal static string Media {
            get {
                return ResourceManager.GetString("Media", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 SimpleSudoku Files (*.simsudo)|*.simsudo|All Files (*.*)|*.* 的本地化字符串。
        /// </summary>
        internal static string SimpleSudoku {
            get {
                return ResourceManager.GetString("SimpleSudoku", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Text Files (*.txt)|*.txt|All Files (*.*)|*.* 的本地化字符串。
        /// </summary>
        internal static string Txt {
            get {
                return ResourceManager.GetString("Txt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Xml Files (*.xml)|*.xml|All Files (*.*)|*.* 的本地化字符串。
        /// </summary>
        internal static string Xml {
            get {
                return ResourceManager.GetString("Xml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Zip Files (*.zip)|*.zip|All Files (*.*)|*.* 的本地化字符串。
        /// </summary>
        internal static string Zip {
            get {
                return ResourceManager.GetString("Zip", resourceCulture);
            }
        }
    }
}
