﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
<<<<<<< HEAD
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }
=======
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;
>>>>>>> master

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(20)]
<<<<<<< HEAD
    public string? Phone { get; set; }

    public int? Age { get; set; }

    public int? UserRoleId { get; set; }
=======
    public string Phone { get; set; } = null!;

    public int? Age { get; set; }

    public int UserRoleId { get; set; }

    [StringLength(20)]
    public string Password { get; set; } = null!;
>>>>>>> master

    [InverseProperty("User")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("User")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("UserRoleId")]
    [InverseProperty("Users")]
<<<<<<< HEAD
    public virtual UserRole? UserRole { get; set; }
=======
    public virtual UserRole UserRole { get; set; } = null!;
>>>>>>> master
}
