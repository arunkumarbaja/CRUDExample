using System.ComponentModel.DataAnnotations;

namespace Entites
{
    /// <summary>
    /// Domain model for storing country details
    /// </summary>
    public class Country
    {
        [Key]
        public Guid CountryID { get; set; }
        [StringLength(50)]
        public string? CountryName { get; set; }

      //  public virtual ICollection<Person>? Persons { get; set; }  //it has the prop details of Person Who is of Particular Country
                                                                     //This is a Navigation Property that is used instead of joins

    }
}
