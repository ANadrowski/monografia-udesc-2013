﻿#pragma checksum "..\..\cadastrarMaterial.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E8F9675641A585686E612C293BA5A4F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace OtimizaCut_v2 {
    
    
    /// <summary>
    /// cadastrarMaterial
    /// </summary>
    public partial class cadastrarMaterial : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox codigoMat;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descricaoMat;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox comprimentoMat;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox larguraMat;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox espessuraMat;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox corMat;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tipoMat;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button salvar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button limpar;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\cadastrarMaterial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox rotacionaMat;
        
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
            System.Uri resourceLocater = new System.Uri("/OtimizaCut_v2;component/cadastrarmaterial.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\cadastrarMaterial.xaml"
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
            this.codigoMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.descricaoMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.comprimentoMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.larguraMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.espessuraMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.corMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tipoMat = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.salvar = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\cadastrarMaterial.xaml"
            this.salvar.Click += new System.Windows.RoutedEventHandler(this.salvar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.limpar = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\cadastrarMaterial.xaml"
            this.limpar.Click += new System.Windows.RoutedEventHandler(this.limpar_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.rotacionaMat = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

