#if BRX_APP
	// base on https://www.bricsys.com/bricscad/help/en_US/V16/DevRef/
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
using _AcDb = Autodesk.AutoCAD.DatabaseServices;
using _AcEd = Autodesk.AutoCAD.EditorInput;
using _AcGe = Autodesk.AutoCAD.Geometry;
//using _AcBrx = Autodesk.AutoCAD.Runtime;
//	AcRx
//	AcWd
#endif
using System;


namespace TruncatedIcosidodecahedron {

	// from: http://through-the-interface.typepad.com/through_the_interface/2015/09/intersecting-autocad-curves-with-a-plane-using-net.html

	public static class MyExtensions {
		///<summary>
		/// Returns an array of Point3d objects from a Point3dCollection.
		///</summary>
		///<returns>An array of Point3d objects.</returns>
		public static _AcGe.Point3d[] ToArray( this _AcGe.Point3dCollection pts ) {
			var res = new _AcGe.Point3d[pts.Count];
			pts.CopyTo( res, 0 );
			return res;
		}

		/// <summary>
		///  Get the intersection points between this planar entity and a curve.
		/// </summary>
		/// <param name="plane" />
		/// <param name="cur">The curve to check intersections against.</param>
		/// <returns>An array of Point3d intersections.</returns>
		public static _AcGe.Point3d[] IntersectWith( this _AcGe.Plane plane, _AcDb.Curve cur ) {
			var pts = new _AcGe.Point3dCollection();

			// Get the underlying GeLib curve
			var gcur = cur.GetGeCurve();

			// Project this curve onto our plane
			var proj = gcur.GetProjectedEntity( plane, plane.Normal ) as _AcGe.Curve3d;
			if( proj != null ) {
				// Create a DB curve from the projected Ge curve
				using( var gcur2 = _AcDb.Curve.CreateFromGeCurve( proj ) ) {
					// Check where it intersects with the original curve: these should be our intersection points on the plane
					cur.IntersectWith( gcur2, _AcDb.Intersect.OnBothOperands, pts, IntPtr.Zero, IntPtr.Zero );
				}
			}
			return pts.ToArray();
		}

		/// <summary>
		///  Test whether a point is on this curve.
		/// </summary>
		/// <param name="cv" />
		/// <param name="pt">The point to check against this curve.</param>
		/// <returns>Boolean indicating whether the point is on the curve.</returns>
		public static bool IsOn( this _AcDb.Curve cv, _AcGe.Point3d pt ) {
			try {
				// Return true if operation succeeds
				var p = cv.GetClosestPointTo( pt, false );
				return (p - pt).Length <= _AcGe.Tolerance.Global.EqualPoint;
			} catch {
				// ignored
			}

			// Otherwise we return false
			return false;
		}
	}

	/// <summary>
	/// <see href="https://forums.autodesk.com/t5/net/do-zoom-extents-in-c-net-autocad/td-p/7735556"/>
	/// by:		Gilles Chanteau
	/// </summary>
	public static class EditorExtension {
		public static void Zoom( this _AcEd.Editor ed, _AcDb.Extents3d ext ) {
			if( ed == null )
				throw new ArgumentNullException( nameof( ed ) );
			using( _AcDb.ViewTableRecord view = ed.GetCurrentView() ) {
				_AcGe.Matrix3d worldToEye = _AcGe.Matrix3d.WorldToPlane( view.ViewDirection ) *
											_AcGe.Matrix3d.Displacement( _AcGe.Point3d.Origin - view.Target ) *
											_AcGe.Matrix3d.Rotation( view.ViewTwist, view.ViewDirection, view.Target );
				ext.TransformBy( worldToEye );
				view.Width = ext.MaxPoint.X - ext.MinPoint.X;
				view.Height = ext.MaxPoint.Y - ext.MinPoint.Y;
				view.CenterPoint = new _AcGe.Point2d(
					(ext.MaxPoint.X + ext.MinPoint.X) / 2.0,
					(ext.MaxPoint.Y + ext.MinPoint.Y) / 2.0 );
				ed.SetCurrentView( view );
			}
		}

		public static void ZoomExtents( this _AcEd.Editor ed ) {
			_AcDb.Database db = ed.Document.Database;
			db.UpdateExt( false );
			_AcDb.Extents3d ext = (short)_AcApCore.Application.GetSystemVariable( "CVPORT" ) == 1 ?
				new _AcDb.Extents3d( db.Pextmin, db.Pextmax ) :
				new _AcDb.Extents3d( db.Extmin, db.Extmax );
			ed.Zoom( ext );
		}
	}

	public static class DocExtension {
		/// <summary>
		/// Change Visual Style
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="visualStyleName"></param>
		/// <see href="https://adndevblog.typepad.com/autocad/2012/03/changing-visual-style-using-autocad-net-api.html"/>
		public static void ChangeVisualStyle( this _AcAp.Document doc, string visualStyleName = "Realistic" ) {
			var db = doc.Database;
			var ed = doc.Editor;
			try {
				using( var tr = db.TransactionManager.StartTransaction() ) {
					if( tr.GetObject( db.ViewportTableId, _AcDb.OpenMode.ForRead ) is _AcDb.ViewportTable vt ) {
						if( tr.GetObject( vt["*Active"], _AcDb.OpenMode.ForWrite ) is _AcDb.ViewportTableRecord vtr ) {
							if( tr.GetObject( db.VisualStyleDictionaryId, _AcDb.OpenMode.ForRead ) is _AcDb.DBDictionary dict && dict.Contains( visualStyleName ) ) {
								vtr.VisualStyleId = dict.GetAt( visualStyleName );
							}
						}
					}
					tr.Commit();
				}
				ed.UpdateTiledViewportsFromDatabase();
			} catch( System.Exception ex ) {
				ed.WriteMessage( ex.Message );
			}
		}

	}

}
