using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Models
{
    public class Images
    {
        public int ImagesId { get; set; }
        public string ImagesDescription { get; set; }
        public string ImagesPath { get; set; }

        public bool Featured { get; set; }
    }


}
