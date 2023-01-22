/* ===============================
 * VISIONARY S.A.S.
 * harveytriana@gmail.com
 * =============================== 

 General configuration that alia to draw a plot. It is independent of 
 the library and graphics, it applies to both SkiaSharp and System.Drawing

 logical coordinates
 (x1, y2) --- (x2, y2) 
 |                  |
 (x2, y1) --- (x2, y1) 
 */
using System;

namespace SKTools;

public enum Skin { Light, Dark }

public class PlotLayout
{
    public string Title { get; set; }
    // COORDINATES
    public float X1 { get; set; }
    public float Y1 { get; set; }
    public float X2 { get; set; }
    public float Y2 { get; set; }
    // CANVAS AREA
    public float Width { get; set; }
    public float Height { get; set; }
    // MESH
    public float TicksX { get; set; }
    public float TicksY { get; set; }
    public float MeshX { get; set; }
    public float MeshY { get; set; }
    // SKIN
    public string MeshColor { get; set; }
    public string TickColor { get; set; }
    public string Backcolor { get; set; }
    public string AxisColor { get; set; }
    public string CurveColor { get; set; }

    // to support inverted plots (loops as general solution)
    public float Lx1 => Sx < 0 ? X2 : X1;
    public float Lx2 => Sx < 0 ? X1 : X2;
    public float Ly1 => Sy < 0 ? Y2 : Y1;
    public float Ly2 => Sy < 0 ? Y1 : Y2;
    // converters
    float Sx;
    float Sy;

    Skin _skin = Skin.Light;
    public Skin Skin {
        get { return _skin; }
        set {
            _skin = value;
            // hard code for example
            if (_skin == Skin.Dark) {
                Backcolor = "000000";
                AxisColor = "686868";
                TickColor = "505050";
                MeshColor = "3A3A3A";
                CurveColor = "64B1C8";
            }
            else {
                Backcolor = "FFFFFF";
                AxisColor = "888888";
                TickColor = "B6B6B6";
                MeshColor = "DCDCDC";
                CurveColor = "64B1C8";
            }
        }
    }

    public PlotLayout()
    {
        Skin = Skin.Light; // default
    }

    public void SetupInterpolation()
    {
        // converter factors
        Sx = (X2 - X1) / Width;
        Sy = (Y2 - Y1) / Height;
    }

    // cartesian to pixels
    public float PixelFromX(float x) => Round((x - X1) / Sx);
    public float PixelFromY(float y) => Round((y - Y1) / Sy);

    // pixel to cartesians   
    public float XFromPixel(float px) => Sx * (px) + X1;
    public float YFromPixel(float py) => Sy * (py) + Y1;

    // fix conversions from cartesian to pixels
    public static float Round(float number)
    {
        return (float)Math.Round(number, MidpointRounding.AwayFromZero);
    }
}


