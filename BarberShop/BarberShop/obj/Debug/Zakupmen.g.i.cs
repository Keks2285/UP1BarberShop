﻿#pragma checksum "..\..\Zakupmen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DD326D9BEA8B9E1DE8E21CF68908AE50EC7B3538988CFA6528A066A28028982B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BarberShop;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BarberShop {
    
    
    /// <summary>
    /// Zakupmen
    /// </summary>
    public partial class Zakupmen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Login;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Seria;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Nomer;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Posts;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Hi;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Email;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Instruments;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Phone;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Zakupmen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Echeiki;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ень д;component/zakupmen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Zakupmen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Login = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Seria = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Nomer = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Posts = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Hi = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Email = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.Instruments = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Zakupmen.xaml"
            this.Instruments.Click += new System.Windows.RoutedEventHandler(this.Instruments_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Phone = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Echeiki = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Zakupmen.xaml"
            this.Echeiki.Click += new System.Windows.RoutedEventHandler(this.Echeiki_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

