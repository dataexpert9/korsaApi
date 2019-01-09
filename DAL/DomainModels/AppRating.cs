using Component.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DomainModels
{
    public class AppRating : BaseModel
    {
        public int Id { get; set; }
        public short Rating { get; set; }
        public WhatCanBeImproved CanImprove { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<AppRatingML> AppRatingMLsList { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
    }

    public class AppRatingML : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public CultureType Culture { get; set; }

        [ForeignKey("AppRating")]
        public int AppRating_Id { get; set; }
        public AppRating AppRating { get; set; }
    }
}
