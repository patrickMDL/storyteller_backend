using System.ComponentModel.DataAnnotations;

namespace StoryTeller.Models;

public class Class
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int Pv_inicial { get; set; }
    [Required]
    public int Pe_inicial { get; set; }
    [Required]
    public int San_inicial { get; set; }
    [Required]
    public int Pv_level { get; set; }
    [Required]
    public int Pe_level { get; set; }
    [Required]
    public int San_level { get; set; }
    [Required]
    public string Class_Ability_id { get; set; }
}