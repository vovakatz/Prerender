using System;

namespace Huge.Prerender.Models
{
    public class SitemapUrl
    {
        public string Loc { get; set; }
        public DateTime LastMod { get; set; }
        public Enums.SitemapChangeFreq ChangeFreq { get; set; }
        public float Priority { get; set; }

        //custom properties
        public int WaitTime { get; set; }
        public string WaitForElementId { get; set; }
    }
}
