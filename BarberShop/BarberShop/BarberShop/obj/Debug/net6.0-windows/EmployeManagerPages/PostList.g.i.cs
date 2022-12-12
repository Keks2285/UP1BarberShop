﻿#pragma checksum "..\..\..\..\EmployeManagerPages\PostList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21CD8BFBC9034CAD027D93EBBB97DF630AC43D0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BarberShop.EmployeManagerPages;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using ScottPlot;
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


namespace BarberShop.EmployeManagerPages {
    
    
    /// <summary>
    /// PostList
    /// </summary>
    public partial class PostList : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\EmployeManagerPages\PostList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PostDg;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\EmployeManagerPages\PostList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTb;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\EmployeManagerPages\PostList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Price;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\EmployeManagerPages\PostList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchBtn;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\EmployeManagerPages\PostList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Creatpost;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BarberShop;component/employemanagerpages/postlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            ((BarberShop.EmployeManagerPages.PostList)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PostDg = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.PostDg.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PostDg_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.PostDg.Sorting += new System.Windows.Controls.DataGridSortingEventHandler(this.PostDg_Sorting);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.PostDg.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PostDg_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NameTb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Price = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.Price.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SearchBtn = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.SearchBtn.Click += new System.Windows.RoutedEventHandler(this.SearchBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Creatpost = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\EmployeManagerPages\PostList.xaml"
            this.Creatpost.Click += new System.Windows.RoutedEventHandler(this.Creatpost_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
