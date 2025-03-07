using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLineHelper : MonoBehaviour
{

}

public class Line
{
    private float x0, y0, x1, y1;

    public Line(float x0, float y0, float x1, float y1)
    {
        this.x0 = x0;
        this.y0 = y0;
        this.y1 = y1;
        this.x1 = x1;
    }

    public void Apply(float newX0, float newY0, float newX1, float newY1)
    {
        this.x0 = newX0;
        this.y0 = newY0;
        this.x1 = newX1;
        this.y1 = newY1;
    }

    public void Apply(Vector3 zero, Vector3 one)
    {
        this.x0 = zero.x;
        this.y0 = zero.z;
        this.x1 = one.x;
        this.y1 = one.z;
    }

    private void plot(float[,,] bitmap, float x, float y, int z, float c)
    {
        int alpha = (int)(c * 255);
        if (alpha > 255) alpha = 255;
        if (alpha < 0) alpha = 0;

        if(x > bitmap.GetLength(0) - 1 || y > bitmap.GetLength(1) - 1)
        {
            Debug.LogWarning("Trying to plot out of bounds, this is a bug");
            return;
        }

        int bottomX = Mathf.RoundToInt(x);
        int topX = bottomX + 1;
        int bottomY = Mathf.RoundToInt(y);
        int topY = bottomY + 1;

        // Debug.Log($"Plotting {x},{y} bottom x {bottomX}, topX {topX}, bottomY {bottomY}, topY {topY}");
        
        if(bottomX < 0 || bottomX >= bitmap.GetLength(0) || bottomY < 0 || bottomY >= bitmap.GetLength(1)
            || topX < 0 || topX >= bitmap.GetLength(0) || topY < 0 || topY >= bitmap.GetLength(1))
        {
            // Debug.LogWarning("WTF");
            return;
        }

        float fX = fpart(x);
        float fY = fpart(y);

        // at the top, use the full value, at the bottom use 1-full
        float topLeft = Average(1 - fX, fY) * c;
        bitmap[bottomX, topY, z] = Mathf.Max(bitmap[bottomX, topY, 0], topLeft);

        float bottomLeft = Average(1 - fX, 1 - fY);
        bitmap[bottomX, bottomY, z] = Mathf.Max(bitmap[bottomX, bottomY, 0], bottomLeft);

        float bottomRight = Average(fX, 1 - fY);
        bitmap[topX, bottomY, z] = Mathf.Max(bitmap[topX, bottomY, 0], bottomRight);
        
        float topRight = Average(fX, fY);
        bitmap[topX, topY, z] = Mathf.Max(bitmap[topX, topY, 0], topRight);
    }

    private float Average(float f1, float f2)
    {
        return (f1 + f2) / 2f;
    }

    int ipart(float x) { return (int)x;}

    int round(float x) {return ipart(x+0.5f);}

    float fpart(float x) {
        if(x<0) return (1-(x-Mathf.Floor(x)));
        return (x-Mathf.Floor(x));
    }

    float rfpart(float x) {
        return 1-fpart(x);
    }

    public void DrawPartial(float[,,] bitmap, int z, float percent)
    {
        // Debug.Log($"Drawing line {x0},{y0} to {x1},{y1} at percentage {percent}");
        float originalX1 = x1;
        float originalY1 = y1;

        x1 = Mathf.Lerp(x0, x1, percent);
        y1 = Mathf.Lerp(y0, y1, percent);

        // Debug.Log($"Lerp applied to line line {x0},{y0} to {x1},{y1} at percentage {percent}");

        draw(bitmap, z);

        x1 = originalX1;
        y1 = originalY1;
    }

    public void draw(float[,,] bitmap, int z) {
        bool steep = Mathf.Abs(y1-y0) > Mathf.Abs(x1-x0);
        float temp;
        if(steep){
            temp=x0; x0=y0; y0=temp;
            temp=x1;x1=y1;y1=temp;
        }
        if(x0>x1){
            temp = x0;x0=x1;x1=temp;
            temp = y0;y0=y1;y1=temp;
        }

        float dx = x1-x0;
        float dy = y1-y0;
        float gradient = dy/dx;

        float xEnd = round(x0);
        float yEnd = y0+gradient*(xEnd-x0);
        float xGap = rfpart(x0+0.5f);
        float xPixel1 = xEnd;
        float yPixel1 = ipart(yEnd);

        if(steep){
            plot(bitmap, yPixel1,   xPixel1, z, rfpart(yEnd)*xGap);
            plot(bitmap, yPixel1+1, xPixel1, z, fpart(yEnd)*xGap);
        }else{
            plot(bitmap, xPixel1,yPixel1, z, rfpart(yEnd)*xGap);
            plot(bitmap, xPixel1, yPixel1+1, z, fpart(yEnd)*xGap);
        }
        float intery = yEnd+gradient;

        xEnd = round(x1);
        yEnd = y1+gradient*(xEnd-x1);
        xGap = fpart(x1+0.5f);
        float xPixel2 = xEnd;
        float yPixel2 = ipart(yEnd);
        if(steep){
            plot(bitmap, yPixel2,   xPixel2, z, rfpart(yEnd)*xGap);
            plot(bitmap, yPixel2+1, xPixel2, z, fpart(yEnd)*xGap);
        }else{
            plot(bitmap, xPixel2, yPixel2, z, (yEnd)*xGap);
            plot(bitmap, xPixel2, yPixel2+1, z, fpart(yEnd)*xGap);
        }

        if(steep){
            for(int x=(int)(xPixel1+1);x<=xPixel2-1;x++){
                plot(bitmap, ipart(intery), x, z, rfpart(intery));
                plot(bitmap, ipart(intery)+1, x, z, fpart(intery));
                intery+=gradient;
            }
        }else{
            for(int x=(int)(xPixel1+1);x<=xPixel2-1;x++){
                plot(bitmap, x,ipart(intery), z, rfpart(intery));
                plot(bitmap, x, ipart(intery)+1, z, fpart(intery));
                intery+=gradient;
            }
        }
    }
}
