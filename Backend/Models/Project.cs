using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Project")] // Specify the table name here

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectID { get; set; }

    [Required]
    public string ProjectName { get; set; }

    // Navigation property for the Sales related to this project
    public ICollection<myOrders> myOrders { get; set; }
}
