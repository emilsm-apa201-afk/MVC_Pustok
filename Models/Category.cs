namespace MVC_Pustokkk.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Featured> Featureds { get; set; } = new List<Featured>();
    }
}
