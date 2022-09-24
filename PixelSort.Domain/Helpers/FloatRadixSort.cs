using System.Collections.Generic;
using System.Drawing;

namespace PixelSort.Domain
{
    public static class ColorExtensions
    {
        public static void BucketSort(this Color[] colors, int n = 360)
        {
            if (n <= 0)
                return;

            // 1) Create n empty buckets
            var buckets = new List<Color>[n];

            for (int i = 0; i < n; i++)
            {
                buckets[i] = new List<Color>();
            }

            // 2) Put array elements in different buckets
            for (int i = 0; i < n; i++)
            {
                float idx = (colors[i].GetHue()) * n;
                buckets[(int)idx].Add(colors[i]);
            }

            // 3) Sort individual buckets
            for (int i = 0; i < n; i++)
            {
                buckets[i].Sort((a,b) => a.GetHue().CompareTo(b.GetHue()));
            }

            // 4) Concatenate all buckets into arr[]
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    colors[index++] = buckets[i][j];
                }
            }
        }

    }
}
