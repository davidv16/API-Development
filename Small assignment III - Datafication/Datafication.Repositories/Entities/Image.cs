namespace Datafication.Repositories.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int IceCreamId { get; set; }
        public string Url { get; set; }

        public IceCream IceCream { get; set; }
    }
}