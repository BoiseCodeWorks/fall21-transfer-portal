namespace cfbTransferPortal.Models
{
    public class Player
    {
      public int Id {get; set;}
      // ? allows the int field to be null
      public int? TeamId {get; set;}
      public string Name {get; set;}
      public string Picture {get; set;}
      public string Position {get; set;}
      public string Class {get; set;}
      public int Height {get; set;}
      public int Weight {get; set;}
      public Team Team {get; set;}

    }
}