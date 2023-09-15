using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("myOrders")] // Specify the table name here
public class myOrders
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SalesID { get; set; }

    [ForeignKey("Project")]
    public int ProjectID { get; set; }

    public DateTime OrderDate { get; set; }
    public string Region { get; set; }
    public string Rep { get; set; }
    public string Item { get; set; }
    public int Units { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Total { get; set; }

    // Navigation property to reference the associated project
    public Project Project { get; set; }
}
