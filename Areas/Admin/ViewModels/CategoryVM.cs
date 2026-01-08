namespace MVC_Pustokkk.Areas.Admin.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductVM> Products { get; set; } = new List<ProductVM>();

    }
}
