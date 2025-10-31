using System;
using System.Collections.Generic;


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

namespace TruncatedIcosidodecahedron {
	public class TruncatedIcosidodecahedron {

		/*
		 * https://en.wikipedia.org/wiki/Truncated_icosidodecahedron
		 * In geometry, the truncated icosidodecahedron is an Archimedean solid, one of thirteen convex isogonal nonprismatic solids
		 * constructed by two or more types of regular polygon faces.
		 * It has 62 faces: 30 squares, 20 regular hexagons, and 12 regular decagons.
		 * It has the most edges and vertices of all Platonic and Archimedean solids, though the snub dodecahedron has more faces.
		 * Of all vertex-transitive polyhedra, it occupies the largest percentage (89.80%) of the volume of a sphere in which it is inscribed,
		 * very narrowly beating the snub dodecahedron (89.63%) and Small Rhombicosidodecahedron (89.23%), and less narrowly beating the
		 * Truncated Icosahedron (86.74%); it also has by far the greatest volume (206.8 cubic units) when its edge length equals 1.
		 * Of all vertex-transitive polyhedra that are not prisms or antiprisms, it has the largest sum of angles (90 + 120 + 144 = 354 degrees)
		 * at each vertex; only a prism or antiprism with more than 60 sides would have a larger sum.
		 * Since each of its faces has point symmetry (equivalently, 180° rotational symmetry), the truncated icosidodecahedron is a zonohedron.
		 */

		public static List<_AcGe.Point3d> GetVertices() {
			var lst = new List<_AcGe.Point3d>();
			var phi = ( 1 + Math.Sqrt( 5 ) ) / 2.0;

			var x = 1 / phi;
			var y = x;
			var z = 3 + phi;
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 2 / phi;
			y = phi;
			z = 1 + phi + phi;
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 1 / phi;
			y = phi * phi;
			z = -1 + phi + phi + phi;
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi + phi - 1;
			y = 2;
			z = 2 + phi;
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi;
			y = 3;
			z = phi + phi;
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );
			/*****************************************************/

			x = 1 / phi;
			y = x;
			z = 3 + phi;
			(x, y, z) = (z, x, y);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 2 / phi;
			y = phi;
			z = 1 + phi + phi;
			(x, y, z) = (z, x, y);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 1 / phi;
			y = phi * phi;
			z = -1 + phi + phi + phi;
			(x, y, z) = (z, x, y);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi + phi - 1;
			y = 2;
			z = 2 + phi;
			(x, y, z) = (z, x, y);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi;
			y = 3;
			z = phi + phi;
			(x, y, z) = (z, x, y);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			/*****************************************************/

			x = 1 / phi;
			y = x;
			z = 3 + phi;
			(x, y, z) = (y, z, x);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 2 / phi;
			y = phi;
			z = 1 + phi + phi;
			(x, y, z) = (y, z, x);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = 1 / phi;
			y = phi * phi;
			z = -1 + phi + phi + phi;
			(x, y, z) = (y, z, x);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi + phi - 1;
			y = 2;
			z = 2 + phi;
			(x, y, z) = (y, z, x);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			x = phi;
			y = 3;
			z = phi + phi;
			(x, y, z) = (y, z, x);
			lst.Add( new _AcGe.Point3d( +x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( +x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, +z ) );
			lst.Add( new _AcGe.Point3d( -x, -y, -z ) );
			lst.Add( new _AcGe.Point3d( -x, +y, -z ) );
			lst.Add( new _AcGe.Point3d( +x, -y, -z ) );

			return lst;
		}

#if NET5_0_OR_GREATER
#pragma warning disable IDE0230
#endif
		public static byte[][] GetFaceSquare() {
		byte[][] squares = {
				new byte[] {  0,  1, 4, 2 },
				new byte[] {  3,  7, 5, 6 },
				new byte[] {  8, 24, 32, 16 },
				new byte[] {  9, 17, 33, 25 },
				new byte[] { 10, 18, 34, 26 },
				new byte[] { 11, 19, 35, 27 },
				new byte[] { 12, 28, 36, 20 },
				new byte[] { 13, 21, 37, 29 },
				new byte[] { 14, 30, 38, 22 },
				new byte[] { 15, 31, 39, 23 },
				new byte[] { 40, 42, 47, 43 },
				new byte[] { 41, 46, 45, 44 },
				new byte[] { 48, 56, 72, 64 },
				new byte[] { 49, 65, 73, 57 },
				new byte[] { 50, 66, 74, 58 },
				new byte[] { 51, 67, 75, 59 },
				new byte[] { 52, 60, 76, 68 },
				new byte[] { 53, 69, 77, 61 },
				new byte[] { 54, 62, 78, 70 },
				new byte[] { 55, 63, 79, 71 },
				new byte[] { 80, 83, 86, 81 },
				new byte[] { 82, 84, 85, 87 },
				new byte[] { 88, 104, 112, 96 },
				new byte[] { 89, 97, 113, 105 },
				new byte[] { 90, 98, 114, 106 },
				new byte[] { 91, 99, 115, 107 },
				new byte[] { 92, 108, 116, 100 },
				new byte[] { 93, 101, 117, 109 },
				new byte[] { 94, 110, 118, 102 },
				new byte[] { 95, 111, 119, 103 },
			};
			return squares;
		}

		public static byte[][] GetFaceHexagon() {
			byte[][] hexagon = {
				new byte[] {  0, 8, 16, 17, 9, 1 },
				new byte[] {  2, 4, 12, 20, 18, 10 },
				new byte[] {  3, 6, 14, 22, 19, 11 },
				new byte[] {  5, 7, 15, 23, 21, 13 },
				new byte[] { 24, 72, 64, 112, 104, 32 },
				new byte[] { 25, 33, 105, 113, 65, 73 },
				new byte[] { 26, 34, 106, 114, 66, 74 },
				new byte[] { 27, 35, 107, 115, 67, 75 },
				new byte[] { 28, 76, 68, 116, 108, 36 },
				new byte[] { 29, 37, 109, 117, 69, 77 },
				new byte[] { 30, 78, 70, 118, 110, 38 },
				new byte[] { 31, 79, 71, 119, 111, 39 },
				new byte[] { 40, 48, 56, 58, 50, 42 },
				new byte[] { 41, 44, 52, 60, 57, 49 },
				new byte[] { 43, 47, 55, 63, 59, 51 },
				new byte[] { 45, 46, 54, 62, 61, 53 },
				new byte[] { 80, 88, 96, 99, 91, 83 },
				new byte[] { 81, 86, 94, 102, 97, 89 },
				new byte[] { 82, 87, 95, 103, 98, 90 },
				new byte[] { 84, 92, 100, 101, 93, 85 },         };
			return hexagon;
		}

		public static byte[][] GetFaceDecagon() {
			byte[][] decagon = {
				new byte[] {  0, 2, 10, 26, 74, 58, 56, 72, 24, 8 },
				new byte[] {  1, 9, 25, 73, 57, 60, 76, 28, 12, 4 },
				new byte[] {  3, 11, 27, 75, 59, 63, 79, 31, 15, 7 },
				new byte[] {  5, 13, 29, 77, 61, 62, 78, 30, 14, 6 },
				new byte[] { 16, 32, 104, 88, 80, 81, 89, 105, 33, 17 },
				new byte[] { 18, 20, 36, 108, 92, 84, 82, 90, 106, 34 },
				new byte[] { 19, 22, 38, 110, 94, 86, 83, 91, 107, 35 },
				new byte[] { 21, 23, 39, 111, 95, 87, 85, 93, 109, 37 },
				new byte[] { 40, 43, 51, 67, 115, 99, 96, 112, 64, 48 },
				new byte[] { 41, 49, 65, 113, 97, 102, 118, 70, 54, 46 },
				new byte[] { 42, 50, 66, 114, 98, 103, 119, 71, 55, 47 },
				new byte[] { 44, 45, 53, 69, 117, 101, 100, 116, 68, 52 },
			};
			return decagon;
		}
#if NET5_0_OR_GREATER
#pragma warning restore IDE0230
#endif

		/// <summary>
		/// PolyFaceMesh vertex numbering is based on base 1
		/// A better way would be to add a dummy vertex at the beginning, so that the vertex definition of the faces starts at 1
		/// </summary>
		/// <param name="tints"></param>
		public static void StepForPface( ref byte[][] tints ) {
			for( int i = 0; i < tints.Length; i++ ) {
				for( int j = 0; j < tints[i].Length; j++ ) {
					tints[i][j] += 1;
				}

			}
		}

	}
}
