﻿#pragma checksum "..\..\..\..\EmployeManager\EmployeManager.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4F93C62CCC01A0F5A21D5DBDDBB6466DBBDB1407"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BarberShop.EmployeMAnager;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace BarberShop.EmployeMAnager {
    
    
    /// <summary>
    /// EmployeManager
    /// </summary>
    public partial class EmployeManager : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Avatar;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FLTb;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EmployeBtn;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PostBtn;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SickLeaveBtn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button VacationBtn;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitBtn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\EmployeManager\EmployeManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame MainFrame;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BarberShop;component/employemanager/employemanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            ((BarberShop.EmployeMAnager.EmployeManager)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            ((BarberShop.EmployeMAnager.EmployeManager)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Avatar = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.FLTb = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.EmployeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            this.EmployeBtn.Click += new System.Windows.RoutedEventHandler(this.EmployeBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PostBtn = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            this.PostBtn.Click += new System.Windows.RoutedEventHandler(this.PostBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SickLeaveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            this.SickLeaveBtn.Click += new System.Windows.RoutedEventHandler(this.SickLeaveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.VacationBtn = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\EmployeManager\EmployeManager.xaml"
            this.VacationBtn.Click += new System.Windows.RoutedEventHandler(this.VacationBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ExitBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.MainFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

