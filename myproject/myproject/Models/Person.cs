using System.ComponentModel.DataAnnotations.Schema;


namespace myproject.Models
{

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }


    }
}
