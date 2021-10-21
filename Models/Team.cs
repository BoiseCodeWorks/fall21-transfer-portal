
using System.ComponentModel.DataAnnotations;

namespace cfbTransferPortal.Models
{
    public class Team
    {
      public int Id {get; set;}
      // REVIEW cannot require the creatorId - it needs to be set by the controller and will fail validation if required here
      public string CreatorId {get; set;}
      [Required]

      public string Name {get; set;}
      [Required]

      public string Conference {get; set;}
      [Required]

      public string Division {get; set;}
      [Required]
      public string Picture {get; set;}
      public Profile Owner {get; set;}
    }
}