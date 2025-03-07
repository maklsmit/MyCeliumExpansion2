using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTerrain : MonoBehaviour
{
    private void Start()
    {
        Terrain t = Terrain.activeTerrain;
        float[,,] map = new float[10, 10, t.terrainData.alphamapLayers];

        for(int x = 0; x < map.GetLength(0); x++)
        {
            for(int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y, 0] = 0f;
                map[x, y, 1] = 0.5f;
                map[x, y, 2] = 0.5f;
            }
        }

        Line line = new Line(1, 1, 7, 7);
        line.draw(map, 0);

        for(int x = 0; x < map.GetLength(0); x++)
        {
            for(int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x, y, 0] != 0f)
                {
                    map[x, y, 1] = 0.0f;
                    map[x, y, 2] = 0.0f;
                }

            }
        }

        t.terrainData.SetAlphamaps(34, 60, map);
        // map[0, 0, 2] = 1f;
        // map[0, 0, 1] = 0.1f;
        // map[0, 0, 0] = 0.1f;
        // // For each point on the alphamap...
        // for (int y = 0; y < 10; y++)
        // {
        //     for (int x = 0; x < 10; x++)
        //     {
        //         // Get the normalized terrain coordinate that
        //         // corresponds to the point.
        //         float normX = x * 1.0f / (10 - 1);
        //         float normY = y * 1.0f / (10 - 1);

        //         // Get the steepness value at the normalized coordinate.
        //         var angle = t.terrainData.GetSteepness(normX, normY);

        //         // Steepness is given as an angle, 0..90 degrees. Divide
        //         // by 90 to get an alpha blending value in the range 0..1.
        //         var frac = angle / 90.0;
        //         map[x, y, 1] = 0f; // (float)frac;
        //         map[x, y, 0] = 0f; // (float)(1 - frac);
        //         map[x, y, 2] = 1f;
        //     }
        // }
        // map[0, 0, 2] = 0f;
        // map[0, 0, 1] = 0f;
        // map[0, 0, 0] = 1f;
        // t.terrainData.SetAlphamaps(65, 65, map);
    }
}
