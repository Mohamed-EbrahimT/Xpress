using FinalProj.Models;

namespace FinalProj.ViewModels
{
    public class ProductDetailed
    {
        public Product productDetailed { get; set; }
        public Review review { get; set; }
        public IEnumerable<Product>relatedproducts { get; set; }
    }
}
