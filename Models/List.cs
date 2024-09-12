using SQLite;

namespace InAndEx.Models
{
    public class List
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Money { get; set; }
        public string Type { get; set; }
    }
}