// (C) Copyright 2022 by izhar@azati.co.il 
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

using System.Collections.Generic;
using TruncatedIcosidodecahedron;


// This line is not mandatory, but improves loading performances
[assembly: _AcTrx.CommandClass( typeof( MyCommands ) )]

namespace TruncatedIcosidodecahedron {

	// This class is instantiated by AutoCAD for each document when
	// a command is called by the user the first time in the context
	// of a given document. In other words, non static data in this class
	// is implicitly per-document!
	public class MyCommands {
		// The CommandMethod attribute can be applied to any public  member 
		// function of any public class.
		// The function should take no arguments and return nothing.
		// If the method is an instance member then the enclosing class is 
		// instantiated for each document. If the member is a static member then
		// the enclosing class is NOT instantiated.
		//
		// NOTE: CommandMethod has overloads where you can provide helpid and
		// context menu.

		[_AcTrx.CommandMethod( "Tdron" )]
		public void Tdron() {
			var doc = _AcApCore.Application.DocumentManager.MdiActiveDocument;
			if( null == doc )
				return;

			var db = doc.Database;

			var vertices = TruncatedIcosidodecahedron.GetVertices();

			using( var tr = doc.TransactionManager.StartTransaction() ) {

				var squareId = GetLayerId( db, tr, "Square", 4 );
				var hexagonId = GetLayerId( db, tr, "Hexagon", 2 );
				var decagonId = GetLayerId( db, tr, "Decagon", 1 );
				var textId = GetLayerId( db, tr, "Text", 5 );

				var ms = (_AcDb.BlockTableRecord)tr.GetObject( _AcDb.SymbolUtilityServices.GetBlockModelSpaceId( db ), _AcDb.OpenMode.ForWrite );

				var polyFace = new _AcDb.PolyFaceMesh();
				polyFace.SetDatabaseDefaults();
				ms.AppendEntity( polyFace );
				tr.AddNewlyCreatedDBObject( polyFace, true );

				var hyper = new _AcDb.HyperLink {
					Name = "https://en.wikipedia.org/wiki/Truncated_icosidodecahedron",
					Description = "Truncated Icosidodecahedron 62= 30[4] + 20[6] + 12[10]"
				};
				polyFace.Hyperlinks.Add( hyper );

				for( var i = 0; i < vertices.Count; i++ ) {
					var txt = new _AcDb.DBText();
					var pln = new _AcGe.Plane( vertices[i], vertices[i].GetAsVector() );
					txt.Normal = pln.Normal;
					txt.Position = vertices[i];
					txt.TextString = (i + 1).ToString();
					txt.Height = 0.15;
					txt.LayerId = textId;
					ms.AppendEntity( txt );
					tr.AddNewlyCreatedDBObject( txt, true );

					var meshVertex = new _AcDb.PolyFaceMeshVertex( vertices[i] );
					polyFace.AppendVertex( meshVertex );
					tr.AddNewlyCreatedDBObject( meshVertex, true );
				}

				var squares = TruncatedIcosidodecahedron.GetFaceSquare();
				TruncatedIcosidodecahedron.StepForPface( ref squares );
				foreach( var square in squares ) {
					AddMyFace( tr, squareId, polyFace, square[0], square[1], square[2], square[3] );
				}

				var hexagons = TruncatedIcosidodecahedron.GetFaceHexagon();
				TruncatedIcosidodecahedron.StepForPface( ref hexagons );
				foreach( var hexagon in hexagons ) {
					AddMyFace( tr, hexagonId, polyFace, hexagon[0], hexagon[1], hexagon[2], -hexagon[3] );
					AddMyFace( tr, hexagonId, polyFace, -hexagon[0], hexagon[3], hexagon[4], hexagon[5] );
				}

				var decagons = TruncatedIcosidodecahedron.GetFaceDecagon();
				TruncatedIcosidodecahedron.StepForPface( ref decagons );
				foreach( var decagon in decagons ) {
					AddMyFace( tr, decagonId, polyFace, decagon[0], decagon[1], decagon[2], -decagon[3] );
					AddMyFace( tr, decagonId, polyFace, -decagon[0], decagon[3], decagon[4], -decagon[5] );
					AddMyFace( tr, decagonId, polyFace, -decagon[0], decagon[5], decagon[6], -decagon[7] );
					AddMyFace( tr, decagonId, polyFace, -decagon[0], decagon[7], decagon[8], decagon[9] );
				}
				tr.Commit();
				doc.ChangeVisualStyle();
				doc.Editor.ZoomExtents();
			}
		}

		private static void AddMyFace( _AcDb.Transaction tr, _AcDb.ObjectId layerId, _AcDb.PolyFaceMesh polyFace, int vertex1, int vertex2, int vertex3, int vertex4 ) {

			var faceRecord = new _AcDb.FaceRecord( (short)vertex1, (short)vertex2, (short)vertex3, (short)vertex4 ) {
				LayerId = layerId
			};
			polyFace.AppendFaceRecord( faceRecord );
			tr.AddNewlyCreatedDBObject( faceRecord, true );
		}

		public static _AcDb.ObjectId GetLayerId( _AcDb.Database db, _AcDb.Transaction tr, string layerName, short colorIndex ) {
			var lt = (_AcDb.LayerTable)tr.GetObject( db.LayerTableId, _AcDb.OpenMode.ForRead );
			if( lt.Has( layerName ) ) {
				return lt[layerName];
			}
			var ltr = new _AcDb.LayerTableRecord { Name = layerName, Color = _AcCm.Color.FromColorIndex( _AcCm.ColorMethod.ByAci, colorIndex ) };
			lt.UpgradeOpen();
			var layerId = lt.Add( ltr );
			tr.AddNewlyCreatedDBObject( ltr, true );
			return layerId;
		}

		//private static void AddFace( _AcDb.Transaction tr, _AcDb.BlockTableRecord ms, List<_AcGe.Point3d> point3ds, _AcDb.ObjectId layerId, params byte[] pnts ) {
		//	if( pnts.Length > 2 ) {
		//		var poly = new _AcDb.Polyline3d();
		//		poly.SetDatabaseDefaults();
		//		poly.PolyType = _AcDb.Poly3dType.SimplePoly;
		//		poly.LayerId = layerId;
		//		ms.AppendEntity( poly );
		//		tr.AddNewlyCreatedDBObject( poly, true );
		//		for( var i = 0; i < pnts.Length; i++ ) {
		//			var polyVert = new _AcDb.PolylineVertex3d( point3ds[pnts[i]] ) {
		//				LayerId = layerId
		//			};
		//			poly.AppendVertex( polyVert );
		//			tr.AddNewlyCreatedDBObject( polyVert, true );
		//		}
		//		poly.Closed = true;
		//	}
		//}

	}
}
