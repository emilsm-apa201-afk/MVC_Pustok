namespace MVC_Pustokkk.Areas.Admin.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FeaturedVM> Featureds { get; set; } = new List<FeaturedVM>();

    }
}
