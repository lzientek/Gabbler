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
    
    public partial class Follow
    {
        public int Id { get; set; }
        public int Id_Follower { get; set; }
        public int Id_User { get; set; }
    
        public virtual User Follower { get; set; }
        public virtual User User { get; set; }
    }
}
