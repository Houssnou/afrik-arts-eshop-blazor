namespace Admin.Data.learning_blazor
{
    public class DemoProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }
        public List<DemoProductDetails> Details { get; set; }
    }

    public class DemoProductDetails
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
