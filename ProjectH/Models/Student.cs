//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectH.Models
{
    using System;

    public partial class Student
    {
        public int Studentid { get; set; }
        public Nullable<int> Adminid { get; set; }
        public string Name { get; set; }
        public Nullable<int> Contactno { get; set; }
    
        public virtual Admin Admin { get; set; }
    }
}
