using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crudv1.Models;

public class DCandidate
{
  public DCandidate(string fullName, string cellPhoneNumber, string email, int age, string bloodGroup, string address)
  {
    Id = new Guid();
    FullName = fullName;
    CellPhoneNumber = cellPhoneNumber;
    Email = email;
    Age = age;
    BloodGroup = bloodGroup;
    Address = address;
  }

  [Key] public Guid Id { get; set; }

  [Column(TypeName = "VARCHAR(100)")] public String FullName { get; set; }

  [Column(TypeName = "VARCHAR(16)")] public string CellPhoneNumber { get; set; }

  [Column(TypeName = "VARCHAR(100)")] public string Email { get; set; }

  public int Age { get; set; }

  [Column(TypeName = "VARCHAR(3)")] public string BloodGroup { get; set; }

  [Column(TypeName = "VARCHAR(100)")] public string Address { get; set; }


}
