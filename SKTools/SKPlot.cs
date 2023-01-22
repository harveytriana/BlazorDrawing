/* ===============================
 * Experimental works
 * harveytriana@gmail.com
 * =============================== 
*/
using SkiaSharp;

namespace SKTools;

public class SKPlot
{
    PlotLayout layout;

    public delegate float F(float x);

    public SKPlot(PlotLayout plotLayout)
    {
        SetPlotLayout(plotLayout);
    }

    public void SetPlotLayout(PlotLayout plotLayout)
    {
        layout = plotLayout;
        layout.SetupInterpolation();
    }

    public void DrawGrid(SKCanvas canvas, bool drawAxesLines = true)
    {
        using var pen = new SKPaint();
        // clear area
        pen.Style = SKPaintStyle.Fill;
        pen.Color = SKColor.Parse(layout.Backcolor);
        canvas.DrawRect(0, 0, layout.Width, layout.Height, pen);

        // lines
        pen.Style = SKPaintStyle.Stroke;
        pen.IsAntialias = true;
        pen.StrokeWidth = 1F;
        pen.IsAntialias = false;

        float x, px;
        float y, py;

        // mesh lines
        pen.Color = SKColor.Parse(layout.MeshColor);
        for (x = layout.Lx1; x <= layout.Lx2; x += layout.MeshX) {
            px = layout.PixelFromX(x);
            canvas.DrawLine(px, 0, px, layout.Height, pen);
        }
        for (y = layout.Ly1; y <= layout.Ly2; y += layout.MeshY) {
            py = layout.PixelFromY(y);
            canvas.DrawLine(0, py, layout.Width, py, pen);
        }
        // tick lines
        pen.Color = SKColor.Parse(layout.TickColor);
        for (x = layout.Lx1; x <= layout.Lx2; x += layout.TicksX) {
            px = layout.PixelFromX(x);
            canvas.DrawLine(px, 0, px, layout.Height, pen);
        }
        for (y = layout.Ly1; y <= layout.Ly2; y += layout.TicksY) {
            py = layout.PixelFromY(y);
            canvas.DrawLine(0, py, layout.Width, py, pen);
        }
        // coda
        pen.Color = SKColor.Parse(layout.MeshColor);
        px = layout.Width - 0.5F;
        py = layout.Height - 0.5F;
        canvas.DrawRect(0, 0, px, py, pen);

        if (drawAxesLines) {
            pen.Color = SKColor.Parse(layout.AxisColor);
            px = layout.PixelFromX(0);
            canvas.DrawLine(px, 0, px, layout.Height, pen);
            py = layout.PixelFromY(0);
            canvas.DrawLine(0, py, layout.Width, py, pen);
        }
    }

    public void DrawFunction(
       SKCanvas canvas,
       F function,
       bool isYX,
       float strokeWidth = 1,
       string curveColor = null)
    {
        var points = new List<SKPoint>();

        float x, y;

        if (isYX) {
            for (y = layout.Ly1; y <= layout.Ly2; y += layout.MeshY) {
                x = function(y);
                points.Add(new SKPoint(layout.PixelFromX(y), layout.PixelFromY(x)));
            }
        }
        else {
            for (x = layout.Lx1; x <= layout.Lx2; x += layout.MeshX) {
                y = function(x);
                points.Add(new SKPoint(layout.PixelFromX(x), layout.PixelFromY(y)));
            }
        }

        using var pen = new SKPaint {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            Color = SKColor.Parse(curveColor ?? layout.CurveColor),
            StrokeWidth = strokeWidth
        };

        var splitLine = SplineBuilder.BezierPath(points.ToArray());
        canvas.DrawPath(splitLine, pen);
    }
}

