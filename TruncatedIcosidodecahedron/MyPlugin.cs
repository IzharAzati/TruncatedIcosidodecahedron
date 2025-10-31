// (C) Copyright 2017 by  
//

#region USING_CAD_REGION_20251030
// ReSharper disable RedundantUsingDirective
#if BRX_APP
	// Base on:	https://developer.bricsys.com/bricscad/help/en_US/V24/DevRef/index.html?page=source%2FdotNET_overview.htm
  using _AcAp = Bricscad.ApplicationServices;
  using _AcApCore = Bricscad.ApplicationServices;
  using _AcBr = Teigha.BoundaryRepresentation;
  using _AcCm = Teigha.Colors;
  using _AcDb = Teigha.DatabaseServices;
  using _AcEd = Bricscad.EditorInput;
  using _AcGe = Teigha.Geometry;
  using _AcGi = Teigha.GraphicsInterface;
  using _AcGs = Teigha.GraphicsSystem;
  using _AcGsk = Bricscad.GraphicsSystem;
  using _AcPl = Bricscad.PlottingServices;
  using _AcBrx = Bricscad.Runtime;
  using _AcTrx = Teigha.Runtime;
  using _AcWnd = Bricscad.Windows;
  using _AdWnd = Bricscad.Windows;
  using _AcRbn = Bricscad.Ribbon;
  using _AcLy = Teigha.LayerManager;
  using _AcIo = Teigha.Export_Import; //Bricsys specific
  using _AcGbl = Bricscad.Global; //Bricsys specific
  using _AcQad = Bricscad.Quad; //Bricsys specific
  using _AcInt = Bricscad.Internal;
  using _AcPb = Bricscad.Publishing;
  using _AcMg = Teigha.ModelerGeometry; //Bricsys specific
  using _AcLic = Bricscad.Licensing; //Bricsys specific
  using _AcMec = Bricscad.MechanicalComponents; //Bricsys specific
  using _AcBim = Bricscad.Bim; //Bricsys specific
  using _AcDm = Bricscad.DirectModeling; //Bricsys specific
  using _AcIfc = Bricscad.Ifc; //Bricsys specific
  using _AcRhn = Bricscad.Rhino; //Bricsys specific
  using _AcCiv = Bricscad.Civil; //Bricsys specific
  using _AcGc = Bricscad.Parametric; //Bricsys specific
  using _AcHlr = Bricscad.Hlr; //Bricsys specific
  //using _AxApp = BricscadApp; //COM
  //using _AxDb = BricscadDb; //COM
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
  using _AcGsk = ZwSoft.ZwCAD.GraphicsSystem;
  using _AcPl = ZwSoft.ZwCAD.PlottingServices;
  using _AcBrx = ZwSoft.ZwCAD.Runtime;
  using _AcTrx = ZwSoft.ZwCAD.Runtime;
  using _AcWnd = ZwSoft.ZwCAD.Windows;
  using _AdWnd = ZwSoft.ZwCAD.Windows;
  //using _AcRbn = ZwSoft.ZwCAD.Ribbon;                 //?????
  //using _AcInt = ZwSoft.ZwCAD.Internal;               //?????
  //using _AcLy =  ZwSoft.ZwCAD.LayerManager;           //?????
  //using _AxApp = ZwSoft.ZwCAD.Interop;        //COM	//?????
  //using _AxDb =  ZwSoft.ZwCAD.Interop.Common; //COM	//?????
	
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
using _AcGsk = Autodesk.AutoCAD.GraphicsSystem;
using _AcPl = Autodesk.AutoCAD.PlottingServices;
using _AcPb = Autodesk.AutoCAD.Publishing;
using _AcBrx = Autodesk.AutoCAD.Runtime;
using _AcTrx = Autodesk.AutoCAD.Runtime;
using _AcWnd = Autodesk.AutoCAD.Windows;
//using _AdWnd = Autodesk.Windows;
//using _AcRbn = Autodesk.AutoCAD.Ribbon;
using _AcInt = Autodesk.AutoCAD.Internal;
using _AcLy = Autodesk.AutoCAD.LayerManager;
//using _AxApp = Autodesk.AutoCAD.Interop;       //COM
//using _AxDb = Autodesk.AutoCAD.Interop.Common; //COM

#if USE_ACAD_MAP
    using _AcMap         = Autodesk.Gis.Map;
    using _AcMapOd       = Autodesk.Gis.Map.ObjectData;
    using MapTable       = Autodesk.Gis.Map.ObjectData.Table;
    using _AcMapCnst     = Autodesk.Gis.Map.Constants;
    using DataType       = Autodesk.Gis.Map.Constants.DataType;
    using _AcMapUt       = Autodesk.Gis.Map.Utilities;
    using _AcMapPlatform = Autodesk.Gis.Map.Platform;
#endif

#if C3D_APP
    using _C3d    = Autodesk.Civil;
    using _C3dAp  = Autodesk.Civil.ApplicationServices;
    using _C3dDb  = Autodesk.Civil.DatabaseServices;
    using _C3dStl = Autodesk.Civil.DatabaseServices.Styles;
    using _C3dSet = Autodesk.Civil.Settings;
    using _C3dSurfaceExtractionSettingsType = Autodesk.Civil.SurfaceExtractionSettingsType;
#endif

#endif
// ReSharper restore RedundantUsingDirective
#endregion USING_CAD_REGION_20251030

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
