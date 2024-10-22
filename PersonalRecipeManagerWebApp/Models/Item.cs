using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalRecipeManagerWebApp.Models;


public partial class Item
{
    public Guid Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    [Column(TypeName = "decimal")]
    public double Cost { get; set; }

    public Item(Guid id) { Id = id; }

    public Item(Guid id, string name, double cost)
    {
        Id = id;
        Name = name;
        Cost = cost;
    }
}
