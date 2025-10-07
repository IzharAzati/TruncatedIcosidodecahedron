// (C) Copyright 2017 by  
//



// ReSharper disable RedundantUsingDirective
#if BRX_APP
	// base on https://www.bricsys.com/bricscad/help/en_US/V16/DevRef/
	// see change:	https://developer.bricsys.com/bricscad/help/en_US/V22/DevRef/
	using _AcAp = Bricscad.ApplicationServices;
	using _AcApCore = Bricscad.ApplicationServices;
	using _AcBr = Teigha.BoundaryRepresentation;
	using _AcCm = Teigha.Colors;
	using _AcDb = Teigha.DatabaseServices;
	using _AcEd = Bricscad.EditorInput;
	using _AcGe = Teigha.Geometry;
	using _AcGi = Teigha.GraphicsInterface;
	using _AcGs = Teigha.GraphicsSystem;
	using _AcPl = Bricscad.PlottingServices;
	using _AcBrx = Bricscad.Runtime;
	using _AcTrx = Teigha.Runtime;
	using _AcWnd = Bricscad.Windows;
#elif ZRX_APP
	// base on ZWCAD_2017_SP2_ZRXSDK:DOTNET_Migration_Manual.chm
 	using _AcAp = ZwSoft.ZwCAD.ApplicationServices;
 	using _AcApCore = ZwSoft.ZwCAD.ApplicationServices;
	//using _AcBr = ZwSoft.ZwCAD.BoundaryRepresentation;
	using _AcCm = ZwSoft.ZwCAD.Colors;
	using _AcDb = ZwSoft.ZwCAD.DatabaseServices;
	using _AcEd = ZwSoft.ZwCAD.EditorInput;
	using _AcGe = ZwSoft.ZwCAD.Geometry;
	using _AcGi = ZwSoft.ZwCAD.GraphicsInterface;
	using _AcGs = ZwSoft.ZwCAD.GraphicsSystem;
	using _AcPl = ZwSoft.ZwCAD.PlottingServices;
	using _AcBrx = ZwSoft.ZwCAD.Runtime;
	using _AcTrx = ZwSoft.ZwCAD.Runtime;	//	AcRx
	using _AcWnd = ZwSoft.ZwCAD.Windows;	//	AcWd
#elif ARX_APP
using _AcAp = Autodesk.AutoCAD.ApplicationServices;
#if UpToAC1024
		using _AcApCore = Autodesk.AutoCAD.ApplicationServices;
#else
using _AcApCore = Autodesk.AutoCAD.ApplicationServices.Core;
#endif
//using _AcBr = Autodesk.AutoCAD.BoundaryRepresentation;
using _AcCm = Autodesk.AutoCAD.Colors;
using _AcDb = Autodesk.AutoCAD.DatabaseServices;
using _AcEd = Autodesk.AutoCAD.EditorInput;
using _AcGe = Autodesk.AutoCAD.Geometry;
using _AcGi = Autodesk.AutoCAD.GraphicsInterface;
using _AcGs = Autodesk.AutoCAD.GraphicsSystem;
using _AcPl = Autodesk.AutoCAD.PlottingServices;
//using _AcBrx = Autodesk.AutoCAD.Runtime;
using _AcTrx = Autodesk.AutoCAD.Runtime;    //	AcRx
using _AcWnd = Autodesk.AutoCAD.Windows;    //	AcWd
using _AcLm = Autodesk.AutoCAD.LayerManager;
using _AcTp = Autodesk.AutoCAD.Windows.ToolPalette;
#endif
// ReSharper restore RedundantUsingDirective

using System;
using System.Reflection;
using TruncatedIcosidodecahedron;

// This line is not mandatory, but improves loading performances
[assembly: _AcTrx.ExtensionApplication( typeof( MyPlugin ) )]

namespace TruncatedIcosidodecahedron {

	public class MyPlugin :_AcTrx.IExtensionApplication {

		void _AcTrx.IExtensionApplication.Initialize() {
			var assem = Assembly.GetExecutingAssembly();
			var version = assem.GetName().Version ?? new Version( 1, 0, 0, 0 );
			var date = new DateTime( 2000, 1, 1 ).AddDays( version.Build ).AddSeconds( version.Revision * 2 );
			_AcApCore.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(
				$"\n{assem.GetName().Name}, Ver={version}, Date={date:yyyy-MM-dd HH:mm} by izhar@azati.co.il\nUse: Tdron\n" );
		}

		void _AcTrx.IExtensionApplication.Terminate() {
			// Do plug-in application clean up here
		}

	}
}
