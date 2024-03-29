//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gabbler.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Follows = new HashSet<Follow>();
            this.Followers = new HashSet<Follow>();
            this.Gabs = new HashSet<Gab>();
            this.Likes = new HashSet<Like>();
        }
    
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Mail { get; set; }
        public string ConnectionId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public int UserImage_id { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<Gab> Gabs { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual UserImage UserImage { get; set; }
    }
}
