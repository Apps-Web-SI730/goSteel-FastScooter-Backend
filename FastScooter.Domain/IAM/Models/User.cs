using FastScooter.Domain.Model;
 
 namespace FastScooter.Domain.IAM.Models;
 
 public class User :ModelBase
 {
     public string Username { get; init; }
     public string PasswordHashed { get; init; }
     public string Role { get; init; }
     
 }