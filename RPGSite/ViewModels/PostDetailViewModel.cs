using RPGSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RPGSite.ViewModels
{
    public class PostDetailViewModel
    {
        public Posts Post { get; set; }

        public DateTime Date { get; set; }

        public List<Comments> Comments { get; set; }

        public Comments Comment { get; set; }

    }
}