using System.Text.Json.Serialization;

namespace Apiapp
{
    public class Habits
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProcentDone { get; set; }

        
        public Habits(int id, string name , string description , int Procent) 
        { 
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProcentDone = Procent;
        }

        public Habits() { }
    }
}
